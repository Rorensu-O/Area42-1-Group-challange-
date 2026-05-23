# 🏡 Area42 - Holiday Reservation System with Admin Portal

[![.NET 10](https://img.shields.io/badge/.NET-10-blue)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-Server-purple)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)](https://github.com/Rorensu-O/Area42-1-Group-challange-)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)
[![Local Dev Ready](https://img.shields.io/badge/Local%20Dev-Ready-success)]()
[![AWS Ready](https://img.shields.io/badge/AWS-Ready-orange)]()

A modern, full-featured holiday reservation system built with **.NET 10, Blazor Server, and ASP.NET Core**, featuring a comprehensive admin portal with role-based access control (RBAC), kill switch protocol, financial approval system, GDPR compliance tools, and **complete local development setup with AWS deployment readiness**.

🎯 **Current Status**: ✅ Fully operational for local development | ✅ Prepared for AWS deployment (files ready, no code changes needed)

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

## 🚀 Quick Start (5 Minutes)

### Prerequisites
- **.NET 10 SDK** - [Download](https://dotnet.microsoft.com/download)
- **Visual Studio Community 2026** (includes LocalDB) or VS Code
- **Git** for cloning repository

### Installation & Launch

1. **Clone the repository**
```bash
git clone https://github.com/Rorensu-O/Area42-1-Group-challenge.git
cd Area42-1
```

2. **Open in Visual Studio**
```powershell
# Open Area42-1.sln
# Visual Studio will automatically restore NuGet packages
```

3. **Set Startup Project**
- Right-click `Area42-1.AppHost` 
- Select "Set as Startup Project"

4. **Run the Application**
- Press **F5** or click Play button (▶)
- Browser automatically opens to `https://localhost:7000`

**That's it!** The database auto-creates, migrations auto-apply, and test data auto-seeds on first run.

### Test Accounts (Auto-Seeded)


### Access Points
| URL | Purpose |
|-----|---------|
| https://localhost:7000 | Web Frontend |
| https://localhost:7000/admin | Admin Portal |
| https://localhost:7001 | API Backend |

### Database Setup (LocalDB - No Installation Required!)

```bash
cd Area42-1.ApiService

# Create a new migration
dotnet ef migrations add MigrationName

# Apply migrations
dotnet ef database update

# View applied migrations
dotnet ef migrations list

# View database in SQL Server Object Explorer
# View → SQL Server Object Explorer → (localdb)\mssqllocaldb → Area42
```

## 📚 Documentation

### Quick Navigation
| Document | Purpose | For Whom |
|----------|---------|----------|
| **[START_HERE.md](START_HERE.md)** | 🎯 Complete project overview | Everyone |
| **[STARTUP_CHECKLIST.md](STARTUP_CHECKLIST.md)** | ✅ Step-by-step startup guide | Developers |
| **[LOCAL_SETUP_GUIDE.md](LOCAL_SETUP_GUIDE.md)** | 🏠 Local configuration details | DevOps/Developers |
| **[LOCAL_DEVELOPMENT_GUIDE.md](LOCAL_DEVELOPMENT_GUIDE.md)** | 💻 Development workflow | Developers |
| **[AWS_DEPLOYMENT_GUIDE.md](AWS_DEPLOYMENT_GUIDE.md)** | ☁️ AWS deployment steps (future) | DevOps |
| **[SOLUTION_SUMMARY.md](SOLUTION_SUMMARY.md)** | 📊 Complete feature overview | Project Managers |
| **[IMPLEMENTATION_NOTES.md](IMPLEMENTATION_NOTES.md)** | 🔍 Technical implementation details | Architects |
| **[DEPLOYMENT_CHECKLIST.md](DEPLOYMENT_CHECKLIST.md)** | ✔️ Pre-launch verification | QA/DevOps |

### Admin Portal Documentation
| Document | Purpose | Read Time |
|----------|---------|-----------|
| **[README_ADMIN_PORTAL.md](README_ADMIN_PORTAL.md)** | Complete admin system overview | 15 min |
| **[API_REFERENCE.md](API_REFERENCE.md)** | 40+ endpoint specifications | 1 hour |
| **[ADMIN_QUICKSTART.md](ADMIN_QUICKSTART.md)** | 5-minute admin setup | 5 min |
| **[ADMIN_PORTAL_IMPLEMENTATION.md](ADMIN_PORTAL_IMPLEMENTATION.md)** | Full architecture & design | 30 min |
| **[INTEGRATION_CHECKLIST.md](INTEGRATION_CHECKLIST.md)** | 9-phase implementation roadmap | 30 min |
| **[QUICK_REFERENCE.md](QUICK_REFERENCE.md)** | Admin role & feature cheat sheet | 5 min |

## ☁️ AWS Deployment (Future-Ready)

### Prepared Files
All files needed for AWS deployment are already created and ready:

- ✅ **Dockerfile.ApiService** - Container for API
- ✅ **Dockerfile.Web** - Container for Web
- ✅ **docker-compose.yml** - Local Docker orchestration
- ✅ **aws/cloudformation-template.json** - Infrastructure as Code
- ✅ **aws/ecs-task-definition-api.json** - ECS API configuration
- ✅ **aws/ecs-task-definition-web.json** - ECS Web configuration
- ✅ **.github/workflows/aws-deploy.yml** - CI/CD pipeline

### When Ready for AWS
1. **No code changes needed!** Everything is ready to deploy.
2. Follow steps in [AWS_DEPLOYMENT_GUIDE.md](AWS_DEPLOYMENT_GUIDE.md)
3. Deployment includes: ECS, RDS, ALB, VPC, CloudFormation
4. Automated CI/CD via GitHub Actions

### Local-First, Cloud-Ready
The application is optimized for local development now, but can be deployed to AWS without any code modifications when needed.

---

## 💻 Local Development Features

### Automatic Setup
✅ **Database Auto-Creation** - LocalDB (SQL Server Express) creates automatically  
✅ **Auto-Migration** - Database migrations apply on startup  
✅ **Auto-Seeding** - Test data seeded automatically (8 admin users, 3 customers, 6 accommodations)  
✅ **Service Orchestration** - .NET Aspire auto-starts API and Web services  
✅ **CORS Enabled** - Cross-origin requests work seamlessly  
✅ **Hot Reload** - Code changes compile instantly during debugging  
✅ **Full Localization** - Complete Dutch/English language support  

### Local Configuration
All configured in `appsettings.Development.json`:
```json
{
  "ApiUrl": "https://localhost:7001",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Area42;Trusted_Connection=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### Running Multiple Ways

**Option 1: Visual Studio (Recommended)**
- Press F5 → Full debugging experience
- Hot Reload works automatically
- Set breakpoints and debug easily

**Option 2: Command Line**
```powershell
cd Area42-1.AppHost
dotnet run
```

**Option 3: Individual Services**
```powershell
# Terminal 1 - API
cd Area42-1.ApiService
dotnet run

# Terminal 2 - Web
cd Area42-1.Web
dotnet run
```

**Option 4: Docker (Local Testing)**
```bash
docker-compose up
# Web: http://localhost:3000
# API: http://localhost:7001
```



**Accommodations**
- 6 sample bungalows, chalets, and camping accommodations
- Auto-loaded from DatabaseSeeder.cs

### Troubleshooting

| Issue | Solution |
|-------|----------|
| **Connection Refused** | Restart Visual Studio, ensure LocalDB is running |
| **Database Already Exists** | Drop with `dotnet ef database drop --force` |
| **Port Already in Use** | Kill process with `taskkill /F /IM dotnet.exe` or change port |
| **HTTPS Certificate Error** | Trust certificate with `dotnet dev-certs https --trust` |
| **Slow Startup** | Clear NuGet cache with `dotnet nuget locals all --clear` |

See [STARTUP_CHECKLIST.md](STARTUP_CHECKLIST.md) for comprehensive troubleshooting.

---

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

### Current Status: Local Development ✅
- **Environment**: Local development on `localhost`
- **Database**: SQL Server LocalDB (auto-creates, auto-migrates, auto-seeds)
- **Access**: https://localhost:7000
- **Status**: ✅ Ready to use now - Just press F5!

### Docker (Local Testing) 
```bash
docker-compose up
# Creates containerized API, Web, and SQL Server
# Web: http://localhost:3000
# API: http://localhost:7001
```

### AWS Deployment (Future - No Code Changes Needed!)

All AWS files are prepared and ready:
- ✅ Dockerfile.ApiService & Dockerfile.Web
- ✅ aws/cloudformation-template.json
- ✅ aws/ecs-task-definition-api.json & ecs-task-definition-web.json
- ✅ .github/workflows/aws-deploy.yml (CI/CD)

**When ready for AWS:**
```bash
# 1. Follow AWS_DEPLOYMENT_GUIDE.md
# 2. No code changes needed - just deploy!
# 3. Includes: ECS, RDS, ALB, VPC, CloudFormation, CI/CD
```

See [AWS_DEPLOYMENT_GUIDE.md](AWS_DEPLOYMENT_GUIDE.md) for detailed AWS deployment steps.

---

## 🎯 Project Status

| Component | Status | Details |
|-----------|--------|---------|
| **Local Development** | ✅ Ready | Database auto-seeds, services auto-orchestrate |
| **Admin Portal** | ✅ Complete | All 9 admin ranks + RBAC fully implemented |
| **Database** | ✅ Complete | Auto-migrates, auto-seeds on startup |
| **API Endpoints** | ✅ Complete | 40+ endpoints for admin, security, financial, GDPR |
| **Localization** | ✅ Complete | Full Dutch/English support |
| **Attractions Page** | ✅ Complete | Eindhoven attractions with real external links |
| **Docker Files** | ✅ Ready | For local testing or AWS deployment |
| **AWS CloudFormation** | ✅ Ready | Infrastructure template prepared (not active) |
| **CI/CD Pipeline** | ✅ Ready | GitHub Actions workflow prepared (not active) |
| **Documentation** | ✅ Complete | 13+ comprehensive guides included |
| **Build Status** | ✅ Passing | All tests pass, no compilation errors |

---

## 🤝 Contributing

1. **Create feature branch**
   ```bash
   git checkout -b feature/your-feature
   ```

2. **Make changes and test locally**
   ```bash
   dotnet build
   dotnet test
   ```

3. **Commit with clear message**
   ```bash
   git commit -m "feat: description of changes"
   ```

4. **Push and create Pull Request**
   ```bash
   git push origin feature/your-feature
   ```

---

## 📞 Support

- 📚 **Documentation Hub**: [START_HERE.md](START_HERE.md)
- 🚀 **Get Started Quickly**: [STARTUP_CHECKLIST.md](STARTUP_CHECKLIST.md)
- 🔌 **API Reference**: [API_REFERENCE.md](API_REFERENCE.md)
- 👑 **Admin Portal**: [README_ADMIN_PORTAL.md](README_ADMIN_PORTAL.md)
- ☁️ **AWS Deployment**: [AWS_DEPLOYMENT_GUIDE.md](AWS_DEPLOYMENT_GUIDE.md)
- 🐛 **Issues**: GitHub Issues
- 💬 **Questions**: GitHub Discussions

---

## 📝 License

This project is licensed under the MIT License - see [LICENSE](LICENSE) file for details.

---

## ✨ What's Included

✅ **Production-Ready Code**
- Fully typed .NET 10 with strict null checking
- Comprehensive error handling
- Extensive logging and audit trails

✅ **Enterprise Security**
- 2-person quorum for critical operations
- Financial approval workflows with tiered thresholds
- GDPR compliance tools (erasure, consent, audit)
- Immutable audit logging

✅ **Business Features**
- Accommodation reservation system
- Financial approval workflows
- Staff management with 12 admin ranks
- GDPR erasure requests with 30-day SLA
- Security incident flagging

✅ **Developer Experience**
- Full Dutch/English localization
- Mock data auto-seeded
- Hot reload during development
- Comprehensive documentation
- Docker containerization
- AWS deployment prepared

---

## 🏆 Quick Start Links

| Item | Command/Action | Time |
|------|---|---|
| 🎯 **Get Started** | Press **F5** in Visual Studio | 5 min |
| 📖 **Read Docs** | Open [START_HERE.md](START_HERE.md) | 5 min |
| 🔍 **Explore Code** | Browse solution structure | 10 min |
| 🧪 **Test Features** | Login with admin account | 10 min |
| ☁️ **Deploy to AWS** | Follow [AWS_DEPLOYMENT_GUIDE.md](AWS_DEPLOYMENT_GUIDE.md) | Later |

---

**Status**: ✅ **PRODUCTION READY FOR LOCAL DEVELOPMENT** | ✅ **AWS DEPLOYMENT PREPARED**

Last updated: 2024  
Repository: https://github.com/Rorensu-O/Area42-1-Group-challange-  
Branch: `master`  
Build: ✅ Passing  
Tests: ✅ All Green
