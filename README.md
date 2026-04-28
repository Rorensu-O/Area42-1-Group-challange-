# Area42 Reservation System

A modern, secure two-domain accommodation reservation system built with .NET 10, Blazor Server, and Entity Framework Core.

## Features

### 🏨 **Accommodation Management**
- **Multiple Accommodation Types**: Bungalows, Chalets, and Camping Sites
- **Strategy Pattern for Pricing**: Each accommodation type has dynamic pricing based on:
  - Guest surcharge per night
  - Weekend multiplier
  - Seasonal discounts (weekly/monthly)
- **Extensible Design**: Easily add new accommodation types without modifying existing code

### 📅 **Reservation System**
- **Advanced Availability Checking**: Real-time booking availability verification
- **Dynamic Price Calculation**: Automatic price computation based on accommodation type and dates
- **Reservation Management**: Create, update, and cancel reservations
- **Guest Information Tracking**: Store guest details and special requests

### 🔐 **Security & Role-Based Access Control**
- **JWT Authentication**: Secure token-based authentication
- **Role-Based Authorization**:
  - **Customer**: Can browse accommodations, create/manage own reservations
  - **Admin**: Full system access, manage all accommodations and reservations
  - **Staff**: View reservations and manage guest check-ins
- **Password Hashing**: SHA-256 hashing with salt for secure password storage
- **Two-Domain Architecture**:
  - **Customer Portal** (`/accommodations`, `/reservations`, `/login`)
  - **Admin Panel** (`/admin`, `/admin/accommodations`, `/admin/reservations`)

### 🎨 **Modern UI/UX**
- **Responsive Design**: Works seamlessly on desktop, tablet, and mobile
- **Dark/Light Mode**: Full theme support with smooth transitions
- **Color Scheme**: Inspired by Booking.com, TUI, KLM, and Center Parcs
  - Primary Blue: `#003580`
  - Accent Green: `#00c0a3`
  - Professional gradients and animations

## Architecture

### Microservices Structure
```
Area42-1 (Solution)
├── Area42-1.ApiService (ASP.NET Core Web API)
│   ├── Models/
│   │   ├── Accommodations/
│   │   ├── Reservations/
│   │   ├── Users/
│   │   ├── Pricing/
│   │   └── Auth/
│   ├── Data/
│   │   ├── Area42Context.cs (EF Core DbContext)
│   │   └── Repositories/
│   ├── Services/
│   │   ├── AccommodationService.cs
│   │   ├── ReservationService.cs
│   │   └── AuthService.cs
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── AccommodationsController.cs
│   │   └── ReservationsController.cs
│   └── Program.cs
├── Area42-1.Web (Blazor Server)
│   ├── Components/
│   │   ├── Pages/
│   │   │   ├── Home.razor
│   │   │   ├── Accommodations.razor
│   │   │   ├── Login.razor
│   │   │   ├── Register.razor
│   │   │   ├── Reservations.razor
│   │   │   └── AdminDashboard.razor
│   │   └── Layout/
│   │       └── MainLayout.razor
│   ├── Services/
│   │   ├── AuthApiClient.cs
│   │   ├── AccommodationApiClient.cs
│   │   └── ReservationApiClient.cs
│   ├── wwwroot/
│   │   └── css/app.css
│   └── Program.cs
├── Area42-1.ServiceDefaults (Shared configuration)
└── Area42-1.AppHost (Orchestration)
```

### Design Patterns Used

#### 1. **Strategy Pattern** - Pricing Calculation
```csharp
public interface IPricingStrategy
{
    decimal CalculatePrice(int numberOfNights, int numberOfGuests, DateTime checkInDate);
    string GetAccommodationType();
}

// Concrete implementations:
// - BungalowPricingStrategy
// - ChaletPricingStrategy
// - CampingSitePricingStrategy
```

#### 2. **Factory Pattern** - Strategy Creation
```csharp
public class PricingStrategyFactory
{
    public static IPricingStrategy CreateStrategy(AccommodationType type)
    {
        return type switch
        {
            AccommodationType.Bungalow => new BungalowPricingStrategy(),
            AccommodationType.Chalet => new ChaletPricingStrategy(),
            AccommodationType.CampingSite => new CampingSitePricingStrategy(),
            _ => throw new ArgumentException($"Unknown accommodation type: {type}")
        };
    }
}
```

#### 3. **Repository Pattern** - Data Access
Implemented for:
- `IAccommodationRepository`
- `IReservationRepository`
- `IUserRepository`

#### 4. **Service Layer Pattern** - Business Logic
- `IAccommodationService`
- `IReservationService`
- `IAuthService`

#### 5. **Dependency Injection**
- Constructor-based DI in all services
- Registered in Program.cs

## Pricing Models

### 🏠 Bungalow Pricing
- **Base Price**: €150/night
- **Guest Surcharge**: €25 per additional guest per night
- **Weekend Multiplier**: 1.2x (Friday-Sunday)
- **Special Discount**: None

### 🏔️ Chalet Pricing
- **Base Price**: €200/night
- **Guest Surcharge**: €35 per additional guest per night
- **Weekend Multiplier**: 1.15x
- **Special Discount**: 10% off for 7+ nights

### ⛺ Camping Site Pricing
- **Base Price**: €35/night
- **Guest Surcharge**: €5 per additional guest per night
- **Weekend Multiplier**: 1.3x
- **Special Discount**: 15% off for 30+ nights

## Getting Started

### Prerequisites
- .NET 10 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2026 or VS Code

### Installation

1. **Clone the repository**
```bash
cd C:\Users\LAURE\source\repos\Area42-1\
```

2. **Set up the database**
```bash
# From API Service directory
dotnet ef migrations add InitialCreate
dotnet ef database update
```

3. **Configure appsettings**

**ApiService/appsettings.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Area42;Trusted_Connection=true;"
  },
  "Jwt": {
    "Key": "Area42ReservationSystemSecretKeyMustBeAtLeast32Characters1234567890",
    "Issuer": "Area42API",
    "Audience": "Area42Client",
    "ExpirationMinutes": 1440
  }
}
```

**Web/appsettings.json:**
```json
{
  "ApiUrl": "https://localhost:7001"
}
```

4. **Run the application**

From Visual Studio:
- Set `Area42-1.Web` as startup project
- Press F5 to run

### Running Separately

**API Service:**
```bash
cd Area42-1.ApiService
dotnet run
# Runs on https://localhost:7001
```

**Web Application:**
```bash
cd Area42-1.Web
dotnet run
# Runs on https://localhost:7000
```

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get JWT token

### Accommodations
- `GET /api/accommodations` - Get all accommodations
- `GET /api/accommodations/{id}` - Get accommodation by ID
- `GET /api/accommodations/type/{type}` - Get by type (Bungalow, Chalet, CampingSite)
- `POST /api/accommodations` - Create accommodation (Admin only)
- `PUT /api/accommodations/{id}` - Update accommodation (Admin only)
- `DELETE /api/accommodations/{id}` - Delete accommodation (Admin only)

### Reservations
- `GET /api/reservations/{id}` - Get reservation details
- `GET /api/reservations/user/{userId}` - Get user's reservations
- `GET /api/reservations/accommodation/{accommodationId}` - Get accommodation reservations (Staff/Admin)
- `GET /api/reservations/availability/check` - Check availability (query: accommodationId, checkIn, checkOut)
- `POST /api/reservations` - Create reservation
- `PUT /api/reservations/{id}` - Update reservation (Staff/Admin)
- `POST /api/reservations/{id}/cancel` - Cancel reservation

### Request Examples

**Register:**
```json
{
  "email": "user@example.com",
  "firstName": "John",
  "lastName": "Doe",
  "password": "SecurePass123!",
  "confirmPassword": "SecurePass123!"
}
```

**Login:**
```json
{
  "email": "user@example.com",
  "password": "SecurePass123!"
}
```

**Create Reservation:**
```json
{
  "accommodationId": "00000000-0000-0000-0000-000000000001",
  "userId": "00000000-0000-0000-0000-000000000002",
  "checkInDate": "2024-03-15",
  "checkOutDate": "2024-03-22",
  "numberOfGuests": 4,
  "guestName": "John Doe",
  "guestEmail": "john@example.com",
  "guestPhone": "+31612345678",
  "specialRequests": "High floor preferred"
}
```

## Security Best Practices

1. **JWT Tokens**: 24-hour expiration by default
2. **Password Hashing**: SHA-256 hashing
3. **CORS**: Configured for cross-domain requests
4. **Authentication**: Required for sensitive endpoints
5. **Authorization**: Role-based access control
6. **Input Validation**: Server-side validation on all endpoints
7. **HTTPS**: Enforced in production

## Deployment

### Azure Deployment

**API Service to Azure App Service:**
```bash
# From ApiService directory
az webapp up --name area42api --resource-group area42-rg --runtime "dotnet:10.0"
```

**Web to Azure App Service:**
```bash
# From Web directory
az webapp up --name area42web --resource-group area42-rg --runtime "dotnet:10.0"
```

**Database to Azure SQL:**
```bash
# Create SQL Server
az sql server create --name area42server --resource-group area42-rg --admin-user sqladmin

# Create database
az sql db create --server area42server --resource-group area42-rg --name Area42DB
```

### Docker Deployment

**Dockerfile for API Service:**
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Area42-1.ApiService/*.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Area42-1.ApiService.dll"]
```

**Docker Compose:**
```yaml
version: '3.8'
services:
  api:
    build:
      context: .
      dockerfile: Dockerfile.API
    ports:
      - "7001:80"
    environment:
      - ConnectionStrings__DefaultConnection=Server=db;Database=Area42;User=sa;Password=YourPassword123

  web:
    build:
      context: .
      dockerfile: Dockerfile.Web
    ports:
      - "7000:80"
    depends_on:
      - api

  db:
    image: mcr.microsoft.com/mssql/server:latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourPassword123
    ports:
      - "1433:1433"
```

## Styling & Theme

### Color Palette
```css
--primary-blue: #003580    /* Main CTA, links */
--accent-green: #00c0a3    /* Success, highlights */
--secondary-blue: #0066cc  /* Hover states */
--light-bg: #ffffff        /* Light mode background */
--dark-bg: #1a1a1a         /* Dark mode background */
```

### Features
- Smooth transitions between light and dark modes
- Mobile-first responsive design
- Accessibility-friendly contrast ratios
- Professional gradients
- Animated hover states

## Database Schema

### Tables
1. **Users** - System users with roles
2. **Accommodations** - Accommodation listings
3. **Reservations** - Booking records with guest information
4. **Indexes** - Performance optimization on frequently queried columns

### Key Relationships
- User has many Reservations
- Accommodation has many Reservations
- Indexes on (AccommodationId, CheckInDate, CheckOutDate) for availability checks

## Testing

### Unit Testing Recommendations
```csharp
// Test pricing calculations
[TestMethod]
public void BungalowPricing_WeekendMultiplier()
{
    var strategy = new BungalowPricingStrategy();
    var price = strategy.CalculatePrice(2, 4, new DateTime(2024, 3, 16)); // Saturday
    Assert.IsTrue(price > 300); // Base * 2 * 1.2
}

// Test availability
[TestMethod]
public async Task IsAccommodationAvailable_OverlappingDates()
{
    // Create reservation March 15-20
    // Check availability March 18-22 (overlapping)
    // Should return false
}
```

## Performance Optimization

1. **Database Indexing**: Strategic indexes on frequently queried columns
2. **Caching**: Redis support ready for accommodation listings
3. **Async/Await**: All I/O operations are asynchronous
4. **EF Core**: Compiled queries for complex operations
5. **Pagination**: Ready for implementing pagination on large result sets

## Future Enhancements

- [ ] Email notifications for reservation confirmations
- [ ] Payment processing integration (Stripe, PayPal)
- [ ] Guest reviews and ratings
- [ ] Cancellation policies
- [ ] Multi-language support
- [ ] Advanced analytics and reporting
- [ ] Mobile app (Blazor Hybrid)
- [ ] Calendar-based availability view
- [ ] Automated refund processing
- [ ] Integration with property management systems

## Troubleshooting

### Database Connection Issues
```bash
# Check LocalDB
sqllocaldb info
sqllocaldb start mssqllocaldb

# Reset migrations
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### JWT Token Issues
- Verify JWT key length (minimum 32 characters)
- Check token expiration settings
- Ensure Authorization header format: `Bearer <token>`

### CORS Issues
- Verify allowed origins in Program.cs
- Check browser console for specific errors
- Enable developer mode for debugging

## Contributing

1. Create feature branches: `git checkout -b feature/accommodation-types`
2. Follow C# coding conventions
3. Add unit tests for new features
4. Submit pull requests with detailed descriptions

## License

This project is for educational purposes. All rights reserved to Area42.

## Support

For issues and questions:
- Create an issue in GitHub Issues
- Contact: support@area42.nl
- Documentation: https://area42docs.example.com

---

**Built with ❤️ using .NET 10, Blazor Server, and Entity Framework Core**

*Last Updated: January 2024*
*Version: 1.0.0*
