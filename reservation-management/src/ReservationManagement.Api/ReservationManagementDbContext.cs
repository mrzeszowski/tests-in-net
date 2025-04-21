
using Microsoft.EntityFrameworkCore;

namespace ReservationManagement.Api;

internal class ReservationManagementDbContext(DbContextOptions<ReservationManagementDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ReservationManagement");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReservationManagementDbContext).Assembly);
    }
}