using Microsoft.EntityFrameworkCore;
using ParkingMetersCA.Domain.Entities;

namespace ParkingMetersCA.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<ParkingMeter> ParkingMeters { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
