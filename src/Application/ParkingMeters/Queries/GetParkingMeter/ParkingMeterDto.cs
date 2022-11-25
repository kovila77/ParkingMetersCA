using ParkingMetersCA.Application.Common.Mappings;
using ParkingMetersCA.Domain.Entities;

namespace ParkingMetersCA.Application.ParkingMeters.Queries.GetParkingMeter;
public class ParkingMeterDto : IMapFrom<ParkingMeter>
{
    public int Id { get; set; }

    public string? Address { get; set; }

    public bool Status { get; set; }

    public int Usages { get; set; }
}
