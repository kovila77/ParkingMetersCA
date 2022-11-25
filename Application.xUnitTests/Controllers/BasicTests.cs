using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit;
using Xunit.Abstractions;

namespace WebApi.AcceptanceTests.Controllers;
public class BasicTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;

    public BasicTests(ITestOutputHelper output)
    {
        _output = output;

        Environment.SetEnvironmentVariable("UseInMemoryDatabase", "true");
        _factory = new WebApplicationFactory<Program>();
    }

    private async Task AddTestData(HttpClient client)
    {
        var createPmUrl = "/api/ParkingMeters";

        var requests = Enumerable.Range(0, 100)
            .Select(x => new StringContent($$"""{ "address": "a{{x}}" }""", Encoding.UTF8, "application/json"))
            .Select(x => new HttpRequestMessage(HttpMethod.Post, createPmUrl)
            {
                Content = x,
            });

        foreach (var request in requests)
        {
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }

    [Theory]
    [InlineData("/api/ParkingMeters")]
    [InlineData("/api/ParkingMeters/1")]
    public async Task Get_EndpointsReturnValuesAfterInsertion(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        await AddTestData(client); // Add data

        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299

        _output.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
