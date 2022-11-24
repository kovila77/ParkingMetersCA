using ParkingMetersCA.Application.Common.Mappings;
using ParkingMetersCA.Domain.Entities;

namespace ParkingMetersCA.Application.ParkingMeters.Queries.GetParkingMetersWithPagination;
public class ParkingMeterDto : IMapFrom<ParkingMeter>
{
    public int Id { get; set; }

    public string? Address { get; set; }
}
