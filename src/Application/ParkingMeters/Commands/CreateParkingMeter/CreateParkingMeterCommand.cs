using MediatR;
using ParkingMetersCA.Application.Common.Interfaces;
using ParkingMetersCA.Domain.Entities;

namespace ParkingMetersCA.Application.ParkingMeters.Commands.CreateParkingMeter;
public record CreateParkingMeterCommand : IRequest<int>
{
    public string? Address { get; init; }
}

public class CreateParkingMeterCommandHandler : IRequestHandler<CreateParkingMeterCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateParkingMeterCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateParkingMeterCommand request, CancellationToken cancellationToken)
    {
        var entity = new ParkingMeter();

        entity.Address = request.Address;

        _context.ParkingMeters.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
