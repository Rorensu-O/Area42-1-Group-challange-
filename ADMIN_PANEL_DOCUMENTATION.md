# Area42 Admin Panel Documentation

## Overview

The Area42 admin panel provides comprehensive management tools for administrators to oversee accommodations, pricing, reservations, and customers. The panel is role-based with granular permission controls.

## Admin Roles & Permissions

### Role Hierarchy

1. **SuperAdmin** - Full system access
   - All dashboard sections
   - Can approve/reject high-value refunds
   - Kill switch access
   - Can manage other admins
   - Financial reports

2. **Admin** - Senior administrator
   - All SuperAdmin features except kill switch and staff management
   - Accommodations, pricing, reservations, customers
   - Approval authority for refunds

3. **SeniorManager** - Property/Operations manager
   - Overview, KPIs, booking stats
   - Staff management
   - Accommodations, pricing, reservations
   - Financial reports

4. **PropertyManager** - Property-level manager
   - Overview, KPIs, property-specific data
   - Accommodations and pricing for assigned properties
   - Reservations and pricing management

5. **BookingManager** - Booking specialist
   - Overview, KPIs, booking management
   - Reservation management only
   - Booking statistics and reports

6. **CustomerSupport** - Support staff
   - Overview, readonly bookings
   - Support inbox
   - Customer management (view/update status)
   - Limited to support tasks

7. **SeniorIntern** - Supervised intern
   - Overview, readonly bookings
   - Support inbox
   - Read-only access

8. **Intern** - Junior intern
   - Overview only
   - Readonly bookings

9. **InternAdmin** - Intern with minor approval
   - Overview, readonly bookings
   - Pending approvals

## Dashboard Sections

### 📊 Overview
- Dashboard KPIs (bookings, revenue, occupancy)
- Recent activity
- System health status
- Quick stats

### 🔒 Security
- Audit logs
- Login attempts
- Kill switch controls (SuperAdmin only)
- Security incidents
- Authorization logs

### 👥 Staff Management
- User management
- Role assignments
- Permission controls
- Staff activity tracking

### 🏠 Accommodations
- List all accommodations
- Add/edit/delete properties
- Set active/inactive status
- Manage accommodation details (rooms, guests, amenities)
- Upload images

### 💰 Pricing
- View pricing rules by accommodation
- Manage seasonal pricing
- Weekend rates
- Long-stay discounts
- Bulk price updates
- Price change history

### 📅 Reservations
- View all reservations
- Filter by status (Confirmed, Pending, Cancelled, Completed)
- Edit reservation details
- Cancel reservations with reason
- View special requests
- Check guest information

### 👨‍👩‍👧‍👦 Customers
- Customer management interface
- Search and filter customers
- View customer history
- Suspend/restore accounts
- Process refunds
- View total customer value
- Customer statistics

### ⚖️ GDPR
- Data export requests
- Deletion requests
- Personal data requests
- GDPR compliance tools
- Data retention management

### 💰 Financial Reports
- Revenue reports
- Booking analytics
- Refund tracking
- Payment methods
- Financial reconciliation
- Tax documentation

## Key Features

### Accommodation Management
- **Add Accommodation**: Create new properties with details
- **Edit Accommodation**: Update property information
- **Delete Accommodation**: Remove accommodations (archived)
- **Bulk Actions**: Apply changes to multiple properties

**Required Fields:**
- Name
- Type (Chalet, Villa, Bungalow, Cottage, Apartment)
- Max Guests
- Bedrooms
- Bathrooms

**Optional Fields:**
- Description
- Image URL
- Active status

### Pricing Management
- **View Current Rates**: See all active pricing rules
- **Price History**: Track price changes over time
- **Seasonal Pricing**: Set rates for specific seasons
- **Bulk Updates**: Apply percentage increases across properties
- **Rate Types**: Base, Seasonal, Weekend, Long-stay

### Reservation Management
- **Filter Reservations**: By date, status, accommodation
- **View Details**: Guest info, special requests, payment status
- **Modify Reservations**: Update dates, guests, pricing
- **Cancel Reservations**: With cancellation reason
- **Confirmation Status**: Track pending vs. confirmed bookings

### Customer Management
- **View Customers**: Browse all customers
- **Search & Filter**: By name, email, status
- **Customer Metrics**: Total bookings, revenue, member since
- **Account Status**: Active, Suspended, Inactive
- **Refund Processing**: Issue refunds with approval workflow

### Refund Processing
- **Refund Types**: Reservation, Cancellation, Complaint, Other
- **Refund Methods**: Original payment, Credit note, Bank transfer
- **Approval Workflow**: Auto-approval for < €500, manual approval for > €500
- **Reason Tracking**: Document refund reasons for compliance
- **Audit Trail**: All refunds logged with approver info

## Authorization Rules

### Refund Approvals
- **< €50**: Automatic approval (all roles with customer_management)
- **€50 - €500**: Manager approval (SeniorManager+)
- **> €500**: Senior manager approval (Admin/SuperAdmin)
- **High-value (> €1000)**: SuperAdmin only

### Kill Switch Access
- **SuperAdmin Only**: Emergency system shutdown
- **Non-reversible**: Requires confirmation
- **Logged**: All kill switch events audited

### Financial Report Access
- **SuperAdmin/Admin**: Full financial data
- **SeniorManager**: Property-level financials
- **PropertyManager**: Assigned property only

## Best Practices

1. **Always provide reasons** when canceling or modifying reservations
2. **Verify guest information** before processing refunds
3. **Use search filters** to reduce accidental changes
4. **Document special requests** for accommodations
5. **Review pricing history** before bulk updates
6. **Archive rather than delete** accommodations when possible
7. **Process refunds promptly** to maintain customer satisfaction
8. **Audit logs regularly** for security and compliance

## Troubleshooting

### Admin Panel Won't Load
- Verify your user has admin claims
- Check authorization in `AdminAuthorizationService`
- Ensure you're logged in with correct credentials

### Permission Denied Errors
- Contact SuperAdmin to verify your role
- Check visible dashboard sections for your role
- Review `AdminAuthorizationService.GetVisibleDashboardSections()`

### Refund Approval Issues
- Verify amount against approval thresholds
- Check your role permissions
- Ensure customer exists in system

## API Integration

The admin panel integrates with the following API endpoints (to be implemented):

```
POST   /api/accommodations                 - Create accommodation
PUT    /api/accommodations/{id}            - Update accommodation
DELETE /api/accommodations/{id}            - Delete accommodation
PATCH  /api/accommodations/{id}/pricing    - Update pricing

GET    /api/reservations                   - List reservations
POST   /api/reservations                   - Create reservation
PUT    /api/reservations/{id}              - Update reservation
DELETE /api/reservations/{id}              - Cancel reservation

GET    /api/customers                      - List customers
GET    /api/customers/{id}                 - Get customer details
POST   /api/customers/{id}/refund          - Process refund

GET    /api/pricing/rules                  - List pricing rules
POST   /api/pricing/rules                  - Create pricing rule
PUT    /api/pricing/rules/{id}             - Update pricing rule
```

## Localization

All admin panel text supports Dutch/English localization through the `@T()` helper:

```csharp
@T("Dutch text", "English text")
```

Translations are managed through the localization service.

## Mock Data

For development/testing, the system seeds mock data including:
- Sample accommodations (Beachfront Villa, Mountain Cottage, City Apartment)
- Test customers with realistic data
- Sample reservations with various statuses
- Admin users with Dutch-style email addresses (firstname.lastname@area42.nl)

See `DatabaseSeeder.cs` for seed data configuration.
