namespace Area42_1.ApiService.Data;

using Microsoft.EntityFrameworkCore;
using Area42_1.ApiService.Models.Accommodations;
using Area42_1.ApiService.Models.Reservations;
using Area42_1.ApiService.Models.Users;

public class Area42Context : DbContext
{
    public Area42Context(DbContextOptions<Area42Context> options) : base(options)
    {
    }

    public DbSet<Accommodation> Accommodations { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Accommodation configuration
        modelBuilder.Entity<Accommodation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.MaxGuests).IsRequired();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });

        // Reservation configuration
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AccommodationId).IsRequired();
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.CheckInDate).IsRequired();
            entity.Property(e => e.CheckOutDate).IsRequired();
            entity.Property(e => e.NumberOfGuests).IsRequired();
            entity.Property(e => e.TotalPrice).HasPrecision(18, 2);
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.GuestName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.GuestEmail).IsRequired().HasMaxLength(200);
            entity.Property(e => e.GuestPhone).HasMaxLength(20);
            entity.Property(e => e.SpecialRequests).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.HasIndex(e => new { e.AccommodationId, e.CheckInDate, e.CheckOutDate });
            entity.HasIndex(e => e.UserId);
        });

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.Role).IsRequired();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.HasIndex(e => e.Email).IsUnique();
        });
    }
}
