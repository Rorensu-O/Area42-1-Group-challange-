# Unified Login Implementation - Area42

## Overview

The Area42 application now features a **unified login flow** where both admin and customer users log in through the same `/login` page. The system automatically routes them to appropriate dashboards based on their account type.

## Login Architecture

### Single Entry Point
- **URL:** `/login`
- Both admin and customer accounts use the same login form
- Account type is determined by:
  1. Email domain (admin users: `@area42.nl`)
  2. Backend validation and token claims

### User Routing Logic

#### Admin Users
- **Email:** Any address ending with `@area42.nl` (e.g., `j.devries@area42.nl`)
- **Redirect Destination:** `/admin` or `/admin-dashboard`
- **Dashboard:** Full admin panel with role-based permissions

#### Customer Users
- **Email:** Any other domain (e.g., `john@example.com`)
- **Redirect Destination:** `/reservations`
- **Access:** Customer reservation management

### Automatic Redirect on Home

When authenticated users visit the home page (`/`):
- Admin users are automatically redirected to `/admin`
- Customer users are automatically redirected to `/reservations`
- Unauthenticated users see login/register options

## Technical Flow

### 1. Login Page (`Area42-1.Web/Components/Pages/Login.razor`)

**Features:**
- Email and password input with validation
- Real-time account type detection based on email domain
- Visual feedback showing detected account type
- Success/error messaging
- Disabled state during login request

**Process:**
```
User enters email/password
  ↓
Validates inputs (required fields)
  ↓
Detects account type (@area42.nl = Admin, else = Customer)
  ↓
Sends POST request to `/api/auth/login`
  ↓
API returns token + IsAdmin + UserRank
  ↓
Calls AuthStateProvider.LoginAsync(token, isAdmin, userRank)
  ↓
Stores token and admin flag in localStorage
  ↓
Redirects to appropriate dashboard
```

### 2. Authentication API (`Area42-1.ApiService/Controllers/AuthController.cs`)

- Endpoint: `POST /api/auth/login`
- Input: `{ email, password }`
- Output: `AuthResponse { Success, Message, Token, User, IsAdmin, UserRank }`

**Backend Logic:**
```csharp
if (email.EndsWith("@area42.nl"))
    return LoginAdminAsync(request);
else
    return LoginCustomerAsync(request);
```

### 3. Auth State Provider (`Area42-1.Web/CustomAuthStateProvider.cs`)

**LoginAsync Method:**
```csharp
public async Task LoginAsync(string token, bool isAdmin = false, string? userRank = null)
{
    // Save token to localStorage
    await SaveTokenToStorageAsync(token);

    // Parse claims from JWT
    var claims = ParseClaimsFromToken(token);

    // Detect/confirm admin status
    var detectedAdmin = isAdmin || /* claim checks */;

    // Store admin flag and rank
    if (detectedAdmin || isAdmin)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "is_admin", "true");
        if (!string.IsNullOrEmpty(userRank))
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "user_rank", userRank);
    }

    // Notify all listeners
    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}
```

**GetAuthenticationStateAsync Method:**
- Retrieves token from localStorage
- Parses JWT claims (sub, email, role, rank, isAdmin, userType)
- Returns ClaimsPrincipal with claims
- Used by `AuthorizeView` and authorization checks

### 4. Home Page Redirect (`Area42-1.Web/Components/Pages/Home.razor`)

**OnInitializedAsync:**
```csharp
// Get current auth state
var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

if (authenticated)
{
    // Check for admin indicators in claims
    var isAdmin = user.Claims.Any(c => 
        (c.Type == "userType" && c.Value == "Admin") || 
        (c.Type == "isAdmin" && c.Value == "true") ||
        (c.Type == "rank" != null));

    // Redirect immediately
    Navigation.NavigateTo(isAdmin ? "/admin" : "/reservations");
}
```

## Data Flow Diagram

```
┌─────────────────────────────────────────────────────────────────┐
│                        LOGIN PAGE                               │
│  (/login)                                                       │
│  - Unified form for admin & customer                            │
│  - Email domain detection                                       │
└────────────────┬────────────────────────────────────────────────┘
                 │ POST /api/auth/login
                 ↓
┌─────────────────────────────────────────────────────────────────┐
│                     API AUTH SERVICE                            │
│  - AuthService.LoginAsync()                                     │
│  - Email domain check → Admin or Customer path                  │
│  - Generate JWT token                                           │
│  - Return AuthResponse { IsAdmin, UserRank }                    │
└────────────────┬────────────────────────────────────────────────┘
                 │ AuthResponse + Token
                 ↓
┌─────────────────────────────────────────────────────────────────┐
│                   CUSTOM AUTH PROVIDER                          │
│  - LoginAsync(token, isAdmin, userRank)                         │
│  - Save token to localStorage                                   │
│  - Parse JWT claims                                             │
│  - Store admin flag & rank                                      │
│  - Notify state change                                          │
└────────────────┬────────────────────────────────────────────────┘
                 │
        ┌────────┴─────────┐
        ↓                  ↓
   ADMIN USER         CUSTOMER USER
   Redirect to        Redirect to
   /admin             /reservations
```

## Routes Summary

| Path | Purpose | Auth Required | User Type |
|------|---------|---------------|-----------|
| `/` | Home/Dashboard | No | Any |
| `/login` | Login page | No | Any |
| `/register` | Registration | No | Any |
| `/admin` | Admin dashboard | Yes | Admin only |
| `/admin-dashboard` | Admin alias | Yes | Admin only |
| `/reservations` | Customer reservations | Yes | Customer only |
| `/accommodations` | Browse accommodations | No | Any |

## Security Features

1. **Token Storage:** JWT stored in localStorage (httpOnly not possible in Blazor WASM)
2. **JWT Claims:** Contains user identity, role, and rank information
3. **Server Validation:** Backend verifies email domain and user type
4. **Role-Based Access:** `AdminAuthorizationService` enforces permissions
5. **Authorization Cascade:** `CascadingAuthenticationState` in `App.razor` provides auth context

## Email Naming Convention

Admin users follow the Dutch naming convention:
- **Pattern:** `[first_letter_firstname].[surname]@area42.nl`
- **Examples:**
  - `j.devries@area42.nl` (Jan de Vries)
  - `m.garcia@area42.nl` (Maria Garcia)
  - `p.muller@area42.nl` (Peter Müller)
  - `a.lemmens@area42.nl` (Anna Lemmens)

This convention provides privacy while maintaining professionalism.

## Troubleshooting

### Issue: User stuck on login page
**Solution:** Check browser console for JWT parsing errors. Ensure token has valid claims.

### Issue: Admin not redirected to dashboard
**Solution:** Verify `@area42.nl` email. Check backend AuthService for email domain check.

### Issue: Claims not appearing in JWT
**Solution:** Verify JWT generation in `GenerateJwtTokenForAdmin` includes rank claim.

### Issue: localStorage clearing
**Solution:** Check browser's privacy settings. Some browsers may restrict localStorage in private modes.

## Future Enhancements

1. **Multi-tenancy:** Support multiple organization domains
2. **SSO Integration:** SAML/OAuth support
3. **MFA:** Two-factor authentication
4. **Password Reset:** Self-service password recovery
5. **Account Linking:** Merge admin and customer accounts

## Testing Credentials

### Admin Account
- **Email:** `j.devries@area42.nl`
- **Password:** (seed password)
- **Expected Route:** `/admin`

### Customer Account
- **Email:** `john.doe@example.com` (or any non-@area42.nl)
- **Password:** (seed password)
- **Expected Route:** `/reservations`

## Related Files

- `Area42-1.Web/Components/Pages/Login.razor` - Login UI
- `Area42-1.Web/Components/Pages/Home.razor` - Home redirect
- `Area42-1.Web/CustomAuthStateProvider.cs` - Auth state management
- `Area42-1.ApiService/Services/AuthService.cs` - Backend auth logic
- `Area42-1.ApiService/Controllers/AuthController.cs` - Auth endpoint
- `Area42-1.Web/Services/AdminAuthorizationService.cs` - Permission checks
- `Area42-1.Web/Components/App.razor` - Auth cascade provider

