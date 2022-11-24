using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using ParkingMetersCA.Application.Common.Interfaces;
using ParkingMetersCA.Application.Common.Mappings;
using ParkingMetersCA.Application.Common.Models;
using ParkingMetersCA.Application.ParkingMeters.Queries.GetParkingMetersWithPagination;

namespace ParkingMetersCA.Application.ParkingMeters.Queries.GetParkingMeters;
public record GetPrarkingMetersWithPaginationQuery : IRequest<PaginatedList<ParkingMeterDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPrarkingMetersWithPaginationQueryHandler : IRequestHandler<GetPrarkingMetersWithPaginationQuery, PaginatedList<ParkingMeterDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPrarkingMetersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ParkingMeterDto>> Handle(GetPrarkingMetersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.ParkingMeters
            .OrderBy(x => x.Address)
            .ProjectTo<ParkingMeterDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
