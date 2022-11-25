using Microsoft.AspNetCore.Mvc;
using ParkingMetersCA.Application.Common.Models;
using ParkingMetersCA.Application.ParkingMeters.Commands.AddUsageOfParkingMeter;
using ParkingMetersCA.Application.ParkingMeters.Commands.CreateParkingMeter;
using ParkingMetersCA.Application.ParkingMeters.Queries.GetParkingMeter;
using ParkingMetersCA.Application.ParkingMeters.Queries.GetParkingMeters;
using ParkingMetersCA.Application.ParkingMeters.Queries.GetParkingMetersWithPagination;

namespace ParkingMetersCA.WebApi.Controllers;
public class ParkingMetersController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ParkingMeterAddressDto>>>
        GetPrarkingMetersWithPagination([FromQuery] GetPrarkingMetersWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ParkingMeterDto>> GetPrarkingMeter(int id)
    {
        return await Mediator.Send(new GetParkingMeterQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateParkingMeterCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("Enable/{id}")]
    public async Task<ActionResult> EnableParkingMeter(int id)
    {
        await Mediator.Send(new EnableParkingMeterCommand(id));

        return NoContent();
    }

    [HttpPut("Disable/{id}")]
    public async Task<ActionResult> DisableParkingMeter(int id)
    {
        await Mediator.Send(new DisableParkingMeterCommand(id));

        return NoContent();
    }

    [HttpPut("AddUsage/{id}")]
    public async Task<ActionResult> AddUsageParkingMeter(int id)
    {
        await Mediator.Send(new AddUsageOfParkingMeterCommand(id));

        return NoContent();
    }
}
