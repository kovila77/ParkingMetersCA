using ParkingMetersCA.Domain.Entities;

namespace ParkingMetersCA.Application.Common.Exceptions;
public class ParkingMeterDisabledException : Exception
{
    public ParkingMeterDisabledException(object key)
    : base($"{nameof(ParkingMeter)} ({key}) status is disabled.")
    {
    }
}
