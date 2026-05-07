# Area42-1 - Holiday Reservation System with Admin Portal

[![.NET 10](https://img.shields.io/badge/.NET-10-blue)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-Server-purple)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

A modern, secure holiday reservation system built with **.NET 10, Blazor Server, and ASP.NET Core**, featuring a comprehensive admin portal with role-based access control (RBAC), kill switch protocol, financial approval system, and GDPR compliance tools.

## 🚀 Features

### 📅 Reservation System
- Holiday accommodation booking and management
- Real-time availability tracking
- Reservation confirmation and cancellation
- User account management (register/login)
- Guest information tracking and special requests

### 👑 Admin Portal (NEW!)
- **Role-Based Access Control (RBAC)** - 9 admin ranks + 3 HR ranks
- **Dashboard Overview** - KPIs, system health, quick access
- **Security Management** - Kill switch protocol with 2-person quorum
- **Staff Management** - Personnel CRUD, rank assignment, internship tracking
- **Financial System** - Transaction approval with tiered thresholds (€150/€500/€2000)
- **GDPR Tools** - Erasure requests (30-day SLA), consent logging, compliance status
- **Audit Logs** - Immutable tracking of all admin actions
- **Security Flags** - SIEM-ready incident flagging

### 🔐 Security Features
- JWT-based authentication with role claims
- 2-person quorum enforcement for critical operations
- 10-minute auto-expiry for emergency requests
- Rate limiting on sensitive operations
- Immutable audit trail (all admin actions logged)
- Financial approval thresholds and dual-sign requirements
- Session management and timeout framework
- PII encryption field configuration

## Architecture

### Solution Structure
```
Area42-1/
├── Area42-1.ApiService/       # ASP.NET Core API backend
│   ├── Controllers/           # 4 API controllers (40+ endpoints)
│   ├── Data/                  # EF Core DbContext
│   ├── Models/                # Domain models (Admin, Reservation, etc.)
│   └── Program.cs             # Service configuration & policies
├── Area42-1.Web/              # Blazor Server frontend
│   ├── Components/            # Razor components (pages, layouts, admin tabs)
│   ├── Services/              # Business logic (Auth, Admin, KillSwitch)
│   ├── Models/                # Client-side models
│   └── Program.cs             # Blazor configuration
├── Area42-1.AppHost/          # .NET Aspire orchestration
└── Area42-1.ServiceDefaults/  # Shared configuration
```

### Technology Stack

| Layer | Technology |
|-------|------------|
| **Runtime** | .NET 10 |
| **Frontend** | Blazor Server (Razor Components) |
| **Backend** | ASP.NET Core 10 |
| **Database** | SQL Server (EF Core) |
| **Auth** | JWT with role claims |
| **Messaging** | SignalR (built-in) |

## Getting Started

### Prerequisites
- .NET 10 SDK - [Download](https://dotnet.microsoft.com/download)
- SQL Server (or LocalDB)
- Visual Studio 2022+ or VS Code

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/Rorensu-O/Area42-1-Group-challenge.git
cd Area42-1
```

2. **Restore dependencies**
```bash
dotnet restore
```

3. **Update database**
```bash
cd Area42-1.ApiService
dotnet ef database update
cd ..
```

4. **Build solution**
```bash
dotnet build
```

5. **Run the application**
```bash
# Run Blazor Web (includes API)
dotnet run --project Area42-1.Web

# Visit: https://localhost:7033
```

### Database Setup

```bash
cd Area42-1.ApiService

# Create a new migration
dotnet ef migrations add MigrationName

# Apply migrations
dotnet ef database update

# View applied migrations
dotnet ef migrations list
```

## 📚 Documentation

Start here for comprehensive guides:

| Document | Purpose | Read Time |
|----------|---------|-----------|
| **[START_HERE.md](START_HERE.md)** | Navigation guide to all docs | 5 min |
| **[README_ADMIN_PORTAL.md](README_ADMIN_PORTAL.md)** | Complete admin portal summary | 15 min |
| **[API_REFERENCE.md](API_REFERENCE.md)** | 40+ endpoint specifications | 1 hour |
| **[ADMIN_QUICKSTART.md](ADMIN_QUICKSTART.md)** | 5-minute developer setup | 5 min |
| **[ADMIN_PORTAL_IMPLEMENTATION.md](ADMIN_PORTAL_IMPLEMENTATION.md)** | Full architecture & design | 30 min |
| **[INTEGRATION_CHECKLIST.md](INTEGRATION_CHECKLIST.md)** | 9-phase implementation roadmap | 30 min |
| **[QUICK_REFERENCE.md](QUICK_REFERENCE.md)** | Admin role & feature cheat sheet | 5 min |

## 🔌 API Endpoints (40+)

### Admin API (`/api/admin`)
```
GET    /users              # List admin users (paginated)
GET    /users/{id}         # Get specific user
POST   /users              # Create admin user
PUT    /users/{id}         # Update admin user
DELETE /users/{id}         # Delete admin user
GET    /audit-logs         # View audit logs
GET    /security-flags     # View security flags
PUT    /security-flags/{id}  # Review flag
```

### Security API (`/api/security`) - Kill Switch Protocol
```
POST   /kill-switch/initiate           # Initiate emergency
GET    /kill-switch/{id}               # Get status
GET    /kill-switch/pending            # List pending
POST   /kill-switch/{id}/confirm       # Confirm (2nd person)
POST   /kill-switch/{id}/execute       # Execute emergency
POST   /kill-switch/{id}/reject        # Reject request
```

### Financial API (`/api/financial`) - Approval System
```
GET    /approval-thresholds            # Get tiers (€150/€500/€2000)
POST   /transactions                   # Submit transaction
GET    /transactions                   # List transactions
POST   /transactions/{id}/approve      # Approve
POST   /transactions/{id}/reject       # Reject
```

### GDPR API (`/api/gdpr`) - Compliance Tools
```
POST   /erasure-requests               # Request erasure (30-day SLA)
GET    /erasure-requests               # List requests
POST   /erasure-requests/{id}/approve  # Approve
POST   /erasure-requests/{id}/reject   # Reject
POST   /consent                        # Record consent
GET    /compliance-status              # Check GDPR status
```

For complete API specifications, see [API_REFERENCE.md](API_REFERENCE.md)

## 👥 Admin Ranks

### Operational Ranks (9)
- **SuperAdmin** (Tier 1) - Full system access
- **Admin** (Tier 1) - Administrative access
- **SeniorManager** (Tier 1) - Financial & GDPR oversight
- **PropertyManager** (Tier 2) - Property management
- **BookingManager** (Tier 2) - Booking oversight
- **CustomerSupport** (Tier 2) - Customer service
- **SeniorIntern** (Tier 3) - Senior intern
- **Intern** (Tier 3) - General intern
- **InternAdmin** (Tier 3) - Intern administrator

### HR Ranks (3)
- **HRManager** - HR administration
- **HREmployee** - HR staff
- **HRIntern** - HR intern

## 📊 Database Schema

### Core Tables (7)
- **AdminUsers** - Admin accounts with ranks, MFA, lockout
- **KillSwitchRequests** - Emergency protocol (2-person quorum)
- **AuditLogEntry** - Immutable log of all admin actions
- **SecurityFlag** - SIEM-ready incident tracking
- **FinancialAuditLog** - Transaction records & approvals
- **GdprErasureRequest** - Data deletion with 30-day SLA
- **ConsentLog** - GDPR consent tracking

## 🧪 Testing

```bash
# Run all tests
dotnet test

# Run specific test class
dotnet test --filter "ClassName=MyTestClass"

# Test admin API endpoints
# See API_REFERENCE.md for curl examples
```

## 🔐 Security Highlights

✅ JWT authentication with role claims  
✅ 2-person quorum for critical operations  
✅ 10-minute auto-expiry for emergency requests  
✅ Immutable audit logging (all admin actions)  
✅ Financial approval thresholds  
✅ GDPR compliance (Art. 7, 32)  
✅ Rate limiting framework  
✅ Session management framework  
✅ PII encryption fields  

## 🚀 Deployment

### Azure Deployment
```bash
# Deploy API Service
az webapp up --name area42api --runtime "dotnet:10.0"

# Deploy Web
az webapp up --name area42web --runtime "dotnet:10.0"
```

### Docker
```bash
docker-compose up
```

See documentation for detailed deployment guides.
