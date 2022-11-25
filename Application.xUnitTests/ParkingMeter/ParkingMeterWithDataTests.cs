using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using ParkingMetersCA.Application.ParkingMeters.Queries.GetParkingMeter;
using Xunit;
using Xunit.Abstractions;

namespace WebApi.AcceptanceTests.Controllers;
public class ParkingMeterWithDataTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    private readonly string _parkingMetersEndPoint = "/api/ParkingMeters";
    private readonly string _enableEp = "Enable";
    private readonly string _disableEp = "Disable";
    private readonly string _addUsageEp = "AddUsage";

    public ParkingMeterWithDataTests(ITestOutputHelper output)
    {
        _output = output;

        Environment.SetEnvironmentVariable("UseInMemoryDatabase", "true");
        _factory = new WebApplicationFactory<Program>();
        AddTestData(_factory.CreateClient()).Wait();
    }

    private async Task AddTestData(HttpClient client)
    {
        var requests = Enumerable.Range(1, 100)
            .Select(x => new StringContent($$"""{ "address": "a{{x}}" }""", Encoding.UTF8, "application/json"))
            .Select(x => new HttpRequestMessage(HttpMethod.Post, _parkingMetersEndPoint)
            {
                Content = x,
            });

        foreach (var request in requests)
        {
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }

    [Fact]
    public async Task Get_ParkingMeter()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(_parkingMetersEndPoint);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task AddUsage_ParkingMeter()
    {
        var client = _factory.CreateClient();
        var id = 1;

        _output.WriteLine("Check for disable...");
        var response = await client.GetAsync($"{_parkingMetersEndPoint}/{id}");
        response.EnsureSuccessStatusCode();        
        var parkingMeterDto = await response.Content.ReadFromJsonAsync<ParkingMeterDto>();
        Assert.NotNull(parkingMeterDto);
        Assert.False(parkingMeterDto.Status);

        _output.WriteLine("Enabling...");
        response = await client.PutAsync($"{_parkingMetersEndPoint}/{_enableEp}/{id}", null);
        response.EnsureSuccessStatusCode();

        _output.WriteLine("Check that enable...");
        response = await client.GetAsync($"{_parkingMetersEndPoint}/{id}");
        response.EnsureSuccessStatusCode();
        parkingMeterDto = await response.Content.ReadFromJsonAsync<ParkingMeterDto>();
        Assert.NotNull(parkingMeterDto);
        Assert.True(parkingMeterDto.Status);

        var oldUsage = parkingMeterDto.Usages;

        _output.WriteLine("Adding usage...");
        response = await client.PutAsync($"{_parkingMetersEndPoint}/{_addUsageEp}/{id}", null);
        response.EnsureSuccessStatusCode();

        _output.WriteLine("Checking usage changes...");
        response = await client.GetAsync($"{_parkingMetersEndPoint}/{id}");
        response.EnsureSuccessStatusCode();
        parkingMeterDto = await response.Content.ReadFromJsonAsync<ParkingMeterDto>();
        Assert.NotNull(parkingMeterDto);
        Assert.True(oldUsage + 1 == parkingMeterDto.Usages);

        _output.WriteLine("Disabling...");
        response = await client.PutAsync($"{_parkingMetersEndPoint}/{_disableEp}/{id}", null);
        response.EnsureSuccessStatusCode();

        _output.WriteLine("Checking that disabled...");
        response = await client.GetAsync($"{_parkingMetersEndPoint}/{id}");
        response.EnsureSuccessStatusCode();
        parkingMeterDto = await response.Content.ReadFromJsonAsync<ParkingMeterDto>();
        Assert.NotNull(parkingMeterDto);
        Assert.False(parkingMeterDto.Status);
    }

    [Fact]
    public async Task Get_ParkingMeters_ResponseContentWithItems()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(_parkingMetersEndPoint);

        response.EnsureSuccessStatusCode(); // Status Code 200-299
        var resultContentString = await response.Content.ReadAsStringAsync();

        Assert.NotEmpty(resultContentString);

        var match = Regex.Match(resultContentString, """items":\s*\[\s*(.+)\s*\]""");

        Assert.True(match.Success && match.Groups.Count > 0 && string.IsNullOrEmpty(match.Groups[1].Value) != true);
    }
}
