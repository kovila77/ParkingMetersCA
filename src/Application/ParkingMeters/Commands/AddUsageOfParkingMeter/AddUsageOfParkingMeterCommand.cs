using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkingMetersCA.Application.Common.Exceptions;
using ParkingMetersCA.Application.Common.Interfaces;
using ParkingMetersCA.Domain.Entities;

namespace ParkingMetersCA.Application.ParkingMeters.Commands.AddUsageOfParkingMeter;

public record AddUsageOfParkingMeterCommand(int Id) : IRequest;

public class AddUsageOfParkingMeterCommandHandler : IRequestHandler<AddUsageOfParkingMeterCommand>
{
    private readonly IApplicationDbContext _context;

    public AddUsageOfParkingMeterCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(AddUsageOfParkingMeterCommand request, CancellationToken cancellationToken)
    {
        var parkingMeter = await _context.ParkingMeters.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (parkingMeter == null)
        {
            throw new NotFoundException(nameof(ParkingMeter), request.Id);
        }

        parkingMeter.Usages++;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
