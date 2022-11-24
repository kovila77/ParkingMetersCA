
namespace ParkingMetersCA.Domain.Entities;
public class ParkingMeter : BaseEntity
{
    public string? Address { get; set; }

    public bool Status { get; set; }

    public int Usages { get; set; }
}
