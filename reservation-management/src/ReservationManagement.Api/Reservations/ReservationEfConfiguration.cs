using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReservationManagement.Api.Reservations;

public sealed class ReservationEfConfiguration : IEntityTypeConfiguration<Reservation>
{
    public static readonly EntityQueryInclude<Reservation> Include = query => query.Include(x => x.Resources);
    
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservation");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Code).HasMaxLength(100);
        builder.Property(x => x.ExpiresAtUtc);
        builder.Property(x => x.IsConfirmed);
        builder.Property(x => x.CreatedAtUtc);
        builder.Property(x => x.UpdatedAtUtc);
        builder.Property(x => x.Version).IsConcurrencyToken();

        // builder.OwnsMany(x => x.Resources, resourceBuilder =>
        // {
        //     resourceBuilder.Property(x => x.Id);
        //     resourceBuilder.WithOwner().HasForeignKey("ReservationId");
        //     resourceBuilder.ToTable("ReservationResource");
        //     resourceBuilder.HasKey("Id", "ReservationId");
        //     
        //     resourceBuilder.HasIndex(x => x.Id).IsUnique();
        // });
        
        // builder.HasMany(x => x.Resources).WithOne().OnDelete(DeleteBehavior.Cascade);
        
        // builder.HasMany<ReservationResource>(x => x.Resources)
        //     .WithOne()
        //     .HasPrincipalKey(x => x.Id)
        //     .HasForeignKey("ReservationId")
        //     .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.Code).IsUnique(); 
    }
}

public sealed class ReservationResourceEfConfiguration : IEntityTypeConfiguration<ReservationResource>
{
    public void Configure(EntityTypeBuilder<ReservationResource> builder)
    {
        builder.ToTable("ReservationResource");
        
        builder.HasKey(x => new { x.ResourceId, x.ReservationId });
        builder.Property(x => x.ResourceId);
        builder.Property(x => x.ReservationId);
        builder.HasIndex(x => x.ResourceId).IsUnique();
        
        //builder.HasOne<Reservation>().WithMany().HasForeignKey(x => x.ReservationId).IsRequired();
    }
}