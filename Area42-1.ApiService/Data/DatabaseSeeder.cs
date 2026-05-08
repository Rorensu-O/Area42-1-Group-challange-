using Area42_1.ApiService.Models.Admin;
using Area42_1.ApiService.Models.Accommodations;
using Area42_1.ApiService.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Area42_1.ApiService.Data;

public static class DatabaseSeeder
{
    /// <summary>
    /// Seeds the database with initial mock data
    /// </summary>
    public static void SeedDatabase(Area42Context context)
    {
        // Only seed if data doesn't exist
        if (context.AdminUsers.Any())
        {
            return;
        }

        // Seed Admin Users
        var adminUsers = GetMockAdminUsers();
        context.AdminUsers.AddRange(adminUsers);
        context.SaveChanges();

        // Seed Regular Users
        var users = GetMockUsers();
        context.Users.AddRange(users);
        context.SaveChanges();

        // Seed Accommodations
        var accommodations = GetMockAccommodations();
        context.Accommodations.AddRange(accommodations);
        context.SaveChanges();

        System.Console.WriteLine("✅ Database seeded successfully!");
    }

    /// <summary>
    /// Get mock admin users (1 SuperAdmin + several others with different roles)
    /// </summary>
    private static List<AdminUser> GetMockAdminUsers()
    {
        return new List<AdminUser>
        {
            // SuperAdmin
            new AdminUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "superadmin@area42.nl",
                FullName = "Jan de Vries",
                PasswordHash = HashPassword("SuperAdmin@123"),
                Rank = AdminRank.SuperAdmin,
                HRRank = null,
                IsEnabled = true,
                IsLocked = false,
                MfaEnabled = false,
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = null,
                DeactivatedAt = null,
                SessionExpiresAt = null
            },

            // Admin 1
            new AdminUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "admin1@area42.nl",
                FullName = "Maria García",
                PasswordHash = HashPassword("Admin@123"),
                Rank = AdminRank.Admin,
                HRRank = null,
                IsEnabled = true,
                IsLocked = false,
                MfaEnabled = false,
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = null,
                DeactivatedAt = null,
                SessionExpiresAt = null
            },

            // Admin 2
            new AdminUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "admin2@area42.nl",
                FullName = "Peter Müller",
                PasswordHash = HashPassword("Admin@456"),
                Rank = AdminRank.Admin,
                HRRank = null,
                IsEnabled = true,
                IsLocked = false,
                MfaEnabled = false,
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = null,
                DeactivatedAt = null,
                SessionExpiresAt = null
            },

            // Senior Manager
            new AdminUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "seniormanager@area42.nl",
                FullName = "Anne Lemmens",
                PasswordHash = HashPassword("Manager@789"),
                Rank = AdminRank.SeniorManager,
                HRRank = null,
                IsEnabled = true,
                IsLocked = false,
                MfaEnabled = false,
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = null,
                DeactivatedAt = null,
                SessionExpiresAt = null
            },

            // Property Manager
            new AdminUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "propertymanager@area42.nl",
                FullName = "Emma de Boer",
                PasswordHash = HashPassword("Property@111"),
                Rank = AdminRank.PropertyManager,
                HRRank = null,
                IsEnabled = true,
                IsLocked = false,
                MfaEnabled = false,
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = null,
                DeactivatedAt = null,
                SessionExpiresAt = null
            },

            // Booking Manager
            new AdminUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "bookingmanager@area42.nl",
                FullName = "Thomas van den Berg",
                PasswordHash = HashPassword("Booking@222"),
                Rank = AdminRank.BookingManager,
                HRRank = null,
                IsEnabled = true,
                IsLocked = false,
                MfaEnabled = false,
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = null,
                DeactivatedAt = null,
                SessionExpiresAt = null
            },

            // Customer Support
            new AdminUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "support@area42.nl",
                FullName = "Lisa Jansen",
                PasswordHash = HashPassword("Support@333"),
                Rank = AdminRank.CustomerSupport,
                HRRank = null,
                IsEnabled = true,
                IsLocked = false,
                MfaEnabled = false,
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = null,
                DeactivatedAt = null,
                SessionExpiresAt = null
            },

            // HR Manager
            new AdminUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "hrmanager@area42.nl",
                FullName = "Sophia Kramer",
                PasswordHash = HashPassword("HRManager@444"),
                Rank = null,
                HRRank = HRRank.HRManager,
                IsEnabled = true,
                IsLocked = false,
                MfaEnabled = false,
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = null,
                DeactivatedAt = null,
                SessionExpiresAt = null
            }
        };
    }

    /// <summary>
    /// Get mock regular users for testing
    /// </summary>
    private static List<User> GetMockUsers()
    {
        return new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                Email = "guest1@example.com",
                FirstName = "John",
                LastName = "Smith",
                PasswordHash = HashPassword("Guest@123"),
                Role = UserRole.Customer,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = Guid.NewGuid(),
                Email = "guest2@example.com",
                FirstName = "Sarah",
                LastName = "Johnson",
                PasswordHash = HashPassword("Guest@456"),
                Role = UserRole.Customer,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = Guid.NewGuid(),
                Email = "staff@area42.nl",
                FirstName = "Robert",
                LastName = "Wilson",
                PasswordHash = HashPassword("Staff@789"),
                Role = UserRole.Staff,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };
    }

    /// <summary>
    /// Get mock accommodations for testing
    /// </summary>
    private static List<Accommodation> GetMockAccommodations()
    {
        return new List<Accommodation>
        {
            new Accommodation
            {
                Id = Guid.NewGuid(),
                Name = "Modern Family Bungalow",
                Description = "Beautiful modern bungalow in Eindhoven with spacious living areas and garden.",
                Type = AccommodationType.Bungalow,
                MaxGuests = 4,
                Bedrooms = 2,
                Bathrooms = 1,
                ImageUrl = "https://images.unsplash.com/photo-1570129477492-45201b8edff0?w=400&h=250&fit=crop",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Accommodation
            {
                Id = Guid.NewGuid(),
                Name = "Luxury Countryside Chalet",
                Description = "Premium chalet near Eindhoven with panoramic views and modern amenities.",
                Type = AccommodationType.Chalet,
                MaxGuests = 6,
                Bedrooms = 3,
                Bathrooms = 2,
                ImageUrl = "https://images.unsplash.com/photo-1564013799919-ab600027ffc6?w=400&h=250&fit=crop",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Accommodation
            {
                Id = Guid.NewGuid(),
                Name = "Nature Camping Experience",
                Description = "Beautiful camping spot in nature near Eindhoven. Perfect for outdoor enthusiasts.",
                Type = AccommodationType.CampingSite,
                MaxGuests = 2,
                Bedrooms = 0,
                Bathrooms = 1,
                ImageUrl = "https://images.unsplash.com/photo-1478131143081-80f7f84ca84d?w=400&h=250&fit=crop",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Accommodation
            {
                Id = Guid.NewGuid(),
                Name = "Cozy Garden Bungalow",
                Description = "Intimate bungalow with beautiful garden in Eindhoven suburb. Great for couples.",
                Type = AccommodationType.Bungalow,
                MaxGuests = 2,
                Bedrooms = 1,
                Bathrooms = 1,
                ImageUrl = "https://images.unsplash.com/photo-1568605114967-8130f3a36994?w=400&h=250&fit=crop",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Accommodation
            {
                Id = Guid.NewGuid(),
                Name = "Alpine Mountain Chalet",
                Description = "Exclusive mountain chalet near Eindhoven with premium facilities and stunning views.",
                Type = AccommodationType.Chalet,
                MaxGuests = 8,
                Bedrooms = 4,
                Bathrooms = 3,
                ImageUrl = "https://images.unsplash.com/photo-1578037014386-3c5f1f7c7325?w=400&h=250&fit=crop",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Accommodation
            {
                Id = Guid.NewGuid(),
                Name = "Forest Camping Adventure",
                Description = "Budget camping in scenic forest area near Eindhoven. Nature lovers' paradise.",
                Type = AccommodationType.CampingSite,
                MaxGuests = 4,
                Bedrooms = 0,
                Bathrooms = 1,
                ImageUrl = "https://images.unsplash.com/photo-1469854523086-cc02fe5d8800?w=400&h=250&fit=crop",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };
    }

    /// <summary>
    /// Hash password using SHA-256
    /// </summary>
    private static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
