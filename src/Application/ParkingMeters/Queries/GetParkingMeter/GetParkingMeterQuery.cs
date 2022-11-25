using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkingMetersCA.Application.Common.Exceptions;
using ParkingMetersCA.Application.Common.Interfaces;
using ParkingMetersCA.Application.ParkingMeters.Queries.GetParkingMeter;
using ParkingMetersCA.Domain.Entities;

public record GetParkingMeterQuery(int Id) : IRequest<ParkingMeterDto>;

public class GetParkingMeterQueryHandler : IRequestHandler<GetParkingMeterQuery, ParkingMeterDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetParkingMeterQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ParkingMeterDto> Handle(GetParkingMeterQuery request, CancellationToken cancellationToken)
    {
        var parkingMeter = await _context.ParkingMeters
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (parkingMeter == null)
        {
            throw new NotFoundException(nameof(ParkingMeter), request.Id);
        }

        return _mapper.Map<ParkingMeterDto>(parkingMeter);
    }
}
