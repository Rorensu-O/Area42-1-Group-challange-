# Area42 Development Setup Guide for Teammates

## Prerequisites
- .NET 10 SDK installed
- Docker Desktop (for containerized deployment)
- SQL Server or Docker (for database)
- Visual Studio 2026 or VS Code
- Git

## Quick Start - Local Development

### 1. Clone the Repository
```bash
git clone https://github.com/Rorensu-O/Area42-1-Group-challenge-.git
cd Area42-1
```

### 2. Run via Aspire Orchestration (Recommended)
The easiest way to run the entire application locally:

```bash
# Install Aspire project templates (one-time)
dotnet workload install aspire

# Run the app
dotnet run --project Area42-1.AppHost
```

This automatically:
- Starts the API service on port 7001
- Starts the Web frontend on port 5000
- Configures service discovery between components
- Manages database connections

### 3. Access the Application
- **Web Frontend**: https://localhost:5000
- **API**: https://localhost:7001
- **Aspire Dashboard**: https://localhost:17360 (for monitoring)

---

## Docker Setup - For Team Deployment

### Prerequisites
- Docker Desktop installed and running
- SQL Server connection details

### Building Images

```bash
# Build API service
docker build -f Dockerfile.api -t area42-api:latest .

# Build Web frontend
docker build -f Dockerfile.web -t area42-web:latest .
```

### Running with Docker Compose
```bash
# Start all services (API, Web, SQL Server)
docker-compose up -d

# Check service status
docker-compose ps

# View logs
docker-compose logs -f

# Stop services
docker-compose down
```

### Docker Service URLs
- **Web Frontend**: http://localhost:5002 or https://localhost:5001
- **API Service**: http://localhost:5000 or https://localhost:7001
- **SQL Server**: localhost:1433 (sa / YourPassword123!)

---

## Environment Configuration

### Local Development (appsettings.Development.json)
Already configured to work with Aspire service discovery.

```json
{
  "ApiUrl": "http://apiservice",
  "Services": {
    "apiservice": {
      "https": "https://localhost:7001",
      "http": "http://localhost:5000"
    }
  }
}
```

### Docker Environment (docker-compose.yml)
- Uses internal Docker network (`area42-network`)
- Services communicate via service names (e.g., `apiservice`, `sqlserver`)
- SQL Server automatically initialized with mock data

---

## Admin Panel Access

### Default Admin Credentials
| Username | Password | Role |
|----------|----------|------|
| j.devries@area42.nl | SuperAdmin@123 | SuperAdmin |
| m.garcia@area42.nl | Admin@123 | Admin |
| p.muller@area42.nl | Admin@456 | Admin |
| a.lemmens@area42.nl | Manager@789 | SeniorManager |

All admin emails follow Dutch corporate format: `firstname.lastname@area42.nl`

### Admin Features
- Accommodation management
- Pricing controls
- Reservation management
- Customer management
- Refund processing
- User authorization with role-based access

---

## Database

### Local Development
- Uses LocalDB by default: `(localdb)\mssqllocaldb`
- Database name: `Area42`
- Auto-initialized with mock data via `DatabaseSeeder.cs`

### Docker
- SQL Server 2022 Express running in container
- Database: `Area42`
- SA user: `sa` / `YourPassword123!`
- Port: 1433 (mapped to host)

### Connection Strings

**Development (LocalDB)**
```
Server=(localdb)\mssqllocaldb;Database=Area42;Trusted_Connection=true;
```

**Docker (SQL Server)**
```
Server=sqlserver,1433;Database=Area42;User Id=sa;Password=YourPassword123!;
```

---

## Troubleshooting

### Admin Panel Won't Load
**Issue**: `InvalidOperationException: Authorization requires a cascading parameter`

**Solution**: 
- Ensure `<CascadingAuthenticationState>` is in `App.razor`
- Clear browser cache (Ctrl+Shift+Delete)
- Hard refresh (Ctrl+F5)

### API Connection Issues
**Issue**: "Cannot connect to API service"

**Solution**:
```bash
# Check if services are running
docker-compose ps

# View service logs
docker-compose logs apiservice

# Restart services
docker-compose restart
```

### Database Connection Errors
**Issue**: "Cannot connect to database"

**Solution**:
```bash
# For LocalDB: Reset database
dotnet ef database drop
dotnet ef database update

# For Docker: Check SQL Server
docker-compose logs sqlserver
docker-compose exec sqlserver sqlcmd -S . -U sa -P YourPassword123!
```

### Service Discovery Not Working
**Issue**: "Cannot resolve service name 'apiservice'"

**Solution**:
- Ensure all services are on the same Docker network
- Check `docker-compose.yml` networks configuration
- Verify service names match in configuration files

---

## Development Workflow

### 1. Make Changes
Edit code in your preferred editor.

### 2. Run Tests
```bash
dotnet test
```

### 3. Run Application
```bash
# Aspire (recommended for development)
dotnet run --project Area42-1.AppHost

# OR Docker (for team deployment testing)
docker-compose up
```

### 4. Debug
- Visual Studio: Set breakpoints and use debug tools
- VS Code: Use Omnisharp debugging
- Docker: Use `docker-compose logs -f` for real-time logs

### 5. Commit & Push
```bash
git add .
git commit -m "Your changes"
git push origin admin-login-test
```

---

## AWS Deployment (For Production)

### Prerequisites
- AWS Account with ECR (Elastic Container Registry)
- ECS cluster or Kubernetes cluster
- RDS instance for SQL Server

### Push Images to ECR
```bash
# Build with registry tag
docker build -f Dockerfile.api -t YOUR_ECR_REPO/area42-api:latest .
docker build -f Dockerfile.web -t YOUR_ECR_REPO/area42-web:latest .

# Push to ECR
docker push YOUR_ECR_REPO/area42-api:latest
docker push YOUR_ECR_REPO/area42-web:latest
```

### Deploy to ECS/Kubernetes
Refer to AWS documentation for:
- Task definitions (ECS)
- Deployment manifests (Kubernetes)
- Service configuration
- Load balancer setup
- SSL certificates

---

## Performance Tips

1. **Enable Output Caching**: Already configured in Program.cs
2. **Use Service Discovery**: Aspire handles service location automatically
3. **Connection Pooling**: SQL Server connection pooling is enabled by default
4. **Compression**: HTTP response compression is configured

---

## Security Notes

- All connections use HTTPS in production
- JWT tokens for API authentication
- Admin role-based access control
- CORS policy configured for allowed origins
- No hardcoded secrets in code (use configuration)

---

## Support & Questions

1. Check existing issues in the repository
2. Review this documentation first
3. Ask in team Slack/Teams channel
4. Create a GitHub issue with detailed error logs

---

## Links

- [.NET 10 Documentation](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10)
- [Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)
- [Docker Documentation](https://docs.docker.com/)
- [Entity Framework Documentation](https://learn.microsoft.com/en-us/ef/core/)
