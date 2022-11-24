using ParkingMetersCA.Application.Common.Interfaces;

namespace ParkingMetersCA.Infrastructure.Services;
public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
