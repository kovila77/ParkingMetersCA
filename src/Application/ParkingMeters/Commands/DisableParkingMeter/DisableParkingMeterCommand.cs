using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkingMetersCA.Application.Common.Exceptions;
using ParkingMetersCA.Application.Common.Interfaces;
using ParkingMetersCA.Domain.Entities;

namespace ParkingMetersCA.Application.ParkingMeters.Commands.AddUsageOfParkingMeter;

public record DisableParkingMeterCommand(int Id) : IRequest;

public class DisableParkingMeterCommandHandler : IRequestHandler<DisableParkingMeterCommand>
{
    private readonly IApplicationDbContext _context;

    public DisableParkingMeterCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DisableParkingMeterCommand request, CancellationToken cancellationToken)
    {
        var parkingMeter = await _context.ParkingMeters.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (parkingMeter == null)
        {
            throw new NotFoundException(nameof(ParkingMeter), request.Id);
        }

        parkingMeter.Status = false;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
