# Admin Panel Implementation - Technical Documentation

## Architecture Overview

### Authorization Flow
```
Request → AuthorizeView 
        → CustomAuthStateProvider
        → Claim Verification (userType, isAdmin, rank)
        → AdminAuthorizationService.GetVisibleDashboardSections()
        → Tab/Component Rendering
```

### Component Hierarchy
```
AdminDashboard.razor (Main container)
├── DashboardOverview.razor (Overview tab)
├── SecurityDashboard.razor (Security tab)
├── StaffManagement.razor (Staff tab)
├── AccommodationsManagement.razor (Accommodations tab) ✨ NEW
│   └── Uses: AccommodationApiClient
├── GdprTools.razor (GDPR tab)
└── FinancialReports.razor (Financial tab)
```

## Key Services

### AdminAuthorizationService
**Location:** `Area42-1.Web/Services/AdminAuthorizationService.cs`

**Key Methods:**
```csharp
// Check specific permission by rank
bool HasPermission(AdminRank? rank, string permission)

// Get financial approval requirements
(AdminRank? requiredApproverRank, bool mfaRequired, decimal autonomousLimit) 
    GetFinancialApprovalPolicy(AdminRank rank, decimal amount)

// Get dashboard sections visible to role
string[] GetVisibleDashboardSections(AdminRank? rank)

// Kill switch permission
bool CanInitiateKillSwitch(AdminRank? rank)

// Audit log viewing
bool CanViewAuditLogs(AdminRank? rank)
```

**Rank Hierarchy:**
- **Tier 1** (Full Access): SuperAdmin (1.1), Admin (1.2)
- **Tier 2** (Management): SeniorManager (1.3), PropertyManager (2.1), BookingManager (2.2), CustomerSupport (2.3)
- **Tier 3** (Limited): SeniorIntern (3.1), Intern (3.2), InternAdmin (IA)
- **Tier 4** (HR): HRManager, HREmployee, HRIntern

### AccommodationApiClient
**Location:** `Area42-1.Web/Services/AccommodationApiClient.cs`

**Methods:**
```csharp
Task<List<AccommodationDto>?> GetAllAsync()
Task<AccommodationDto?> GetByIdAsync(Guid id)
Task<List<AccommodationDto>?> GetByTypeAsync(string type)
```

**DTO:**
```csharp
public class AccommodationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public int MaxGuests { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public string ImageUrl { get; set; }
    public bool IsActive { get; set; }
}
```

## Custom Authentication

### CustomAuthStateProvider
**Location:** `Area42-1.Web/CustomAuthStateProvider.cs`

Implements `AuthenticationStateProvider` and:
- Reads JWT token from session/local storage
- Parses claims (userType, isAdmin, rank, etc.)
- Provides `AuthenticationState` to Blazor authorization system

**Key Claims:**
- `userType`: "Admin" or "Customer"
- `isAdmin`: "true" or "false"
- `rank`: AdminRank enum value
- `email`: User's email address
- Standard JWT claims (iss, sub, aud, exp, etc.)

## Admin Models

### AdminRank Enum
**Location:** `Area42-1.ApiService/Models/Admin/AdminModels.cs`

```csharp
public enum AdminRank
{
    SuperAdmin = 1,           // 1.1 - System level
    Admin = 2,                // 1.2 - Full operations
    SeniorManager = 3,        // 1.3 - Management
    PropertyManager = 4,      // 2.1 - Properties only
    BookingManager = 5,       // 2.2 - Bookings only
    CustomerSupport = 6,      // 2.3 - Support only
    SeniorIntern = 7,         // 3.1 - Supervised
    Intern = 8,               // 3.2 - Read-only
    InternAdmin = 9           // IA - Queued writes
}
```

### HRRank Enum
Used for HR module, separate from main admin ranks

## Database Seeding

### Mock Admin Accounts
**Location:** `Area42-1.ApiService/Data/DatabaseSeeder.cs`

**Email Format:** `{firstInitial}.{lastname}@area42.nl`

**Example:**
- Jan de Vries → `j.devries@area42.nl`
- Marie Garcia → `m.garcia@area42.nl`
- Patrick Müller → `p.muller@area42.nl`

**Accounts Seeded:** 13 primary + 2 test accounts (disabled, locked)

**Password Format:** `{Role}@{uniqueNumber}`
- SuperAdmin account: `SuperAdmin@123`
- Admin accounts: `Admin@456`
- Role-specific: `PropertyManager@456`, `BookingManager@789`, etc.

## New Blazor Components

### AccommodationsManagement.razor
**Location:** `Area42-1.Web/Components/AdminComponents/AccommodationsManagement.razor`

**Features:**
- Real-time accommodation listing
- Search functionality (by name/description)
- Type filtering (Chalet, Villa, Bungalow, etc.)
- Active/inactive status filtering
- Responsive table with sorting indicators
- Bilingual UI support (Dutch/English)

**Data Flow:**
```
Component Initialization
        ↓
OnInitializedAsync() → LoadAccommodations()
        ↓
AccommodationApiClient.GetAllAsync()
        ↓
API Request → /api/accommodations
        ↓
Response Mapping to List<AccommodationDto>
        ↓
UI Rendering with Filters
```

**Key Methods:**
```csharp
protected override async Task OnInitializedAsync()
private async Task LoadAccommodations()
private List<AccommodationDto> GetFilteredAccommodations()
private void ShowAddModal()  // Placeholder
```

**Extensibility Points:**
- `ShowAddModal()` → Connect to modal dialog for new accommodations
- `ShowEditModal()` → Connect to edit form
- `ShowPriceModal()` → Connect to pricing dialog
- `ShowDeleteConfirm()` → Connect to delete confirmation
- Add delete/edit API calls to AccommodationApiClient

## Role-Based Access Control Implementation

### Visible Sections by Role

**SuperAdmin:**
- All sections including system config
- Kill switch, audit logs, security flags
- Full financial and GDPR access

**Admin:**
- All operational sections except system config
- Kill switch, audit logs
- Full financial and GDPR access

**SeniorManager:**
- Accommodations, Pricing, Financial, Staff

**PropertyManager:**
- Accommodations, Pricing only

**BookingManager:**
- Bookings and booking stats only

**CustomerSupport:**
- Read-only bookings and support inbox

**Interns:**
- Read-only overview and bookings (supervised)

### Implementation Pattern
```csharp
@if (visibleSections.Contains("accommodations_management"))
{
    <li class="nav-item">
        <a class="nav-link @(activeTab == "accommodations" ? "active" : "")"
           @onclick='() => SelectTab("accommodations")'>
            🏠 Accommodaties
        </a>
    </li>
}

@if (activeTab == "accommodations" && visibleSections.Contains("accommodations_management"))
{
    <AccommodationsManagement />
}
```

## Error Handling & Logging

### Logging
```csharp
@inject ILogger<AccommodationsManagement> Logger

// In component:
Logger.LogError(ex, "Error loading accommodations");
Logger.LogInformation("Add new accommodation modal would show here");
```

### Error Display
- UI toast/banner for user-facing errors
- Console logging for debugging
- Server-side validation (API should validate all operations)

## Security Considerations

### Server-Side Validation
All API endpoints MUST validate:
1. User is authenticated
2. User's rank has permission for operation
3. Operation doesn't exceed financial limits
4. MFA challenge if required

### Example (pseudo-code):
```csharp
[Authorize]
[HttpPost("api/accommodations")]
public async Task<IActionResult> CreateAccommodation(CreateAccommodationDto dto)
{
    var userId = User.FindFirst("sub")?.Value;
    var rank = User.FindFirst("rank") as AdminRank?;

    // Server-side permission check
    if (!_authService.HasPermission(rank, "property_crud"))
        return Unauthorized();

    // Validate input
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    // Create and persist
    var accommodation = _mapper.Map<Accommodation>(dto);
    await _context.Accommodations.AddAsync(accommodation);
    await _context.SaveChangesAsync();

    return Created($"/api/accommodations/{accommodation.Id}", 
                   _mapper.Map<AccommodationDto>(accommodation));
}
```

### Client-Side Security
- Razor components should NOT contain sensitive business logic
- All authorization checks done server-side
- UI controls only provide UX guidance (visibility of buttons)
- Disabled buttons are NOT security - always validate server-side

## Testing Recommendations

### Unit Tests
- Test `AdminAuthorizationService` permission matrix
- Test claim parsing in `CustomAuthStateProvider`
- Test filter logic in `AccommodationsManagement`

### Integration Tests
- Test admin dashboard authorization flow
- Test API client methods with mock HTTP responses
- Test component rendering with various user roles

### E2E Tests
- Test complete admin login flow
- Test accommodation management workflow (list → edit → save)
- Test permission denied scenarios

## Future Enhancements

### Phase 2
- [ ] Full CRUD UI for accommodations (add/edit/delete forms)
- [ ] Pricing management component
- [ ] Reservation management interface
- [ ] Customer management interface
- [ ] Modal dialogs for operations

### Phase 3
- [ ] Bulk operations (batch price updates)
- [ ] Import/Export (CSV accommodation import)
- [ ] Advanced reporting (occupancy rates, revenue trends)
- [ ] Scheduled operations (seasonal pricing auto-apply)
- [ ] Audit log viewer

### Phase 4
- [ ] Real-time notifications (admin alerts)
- [ ] Analytics dashboard (charts, KPIs)
- [ ] Integration with external systems
- [ ] AI-powered recommendations
- [ ] Mobile admin app

## Deployment Checklist

- [ ] Database has been seeded with new admin emails
- [ ] `AdminAuthorizationService` properly configured
- [ ] API endpoints protected with `[Authorize]`
- [ ] Server-side permission validation implemented
- [ ] SSL certificates configured for HTTPS
- [ ] Logging and monitoring enabled
- [ ] Audit trail recording active
- [ ] MFA configured for sensitive operations
- [ ] Rate limiting configured
- [ ] CORS properly configured for admin subdomain/path

---

**Documentation Version:** 1.0
**Last Updated:** 2024
**Status:** Ready for Phase 2 Implementation
