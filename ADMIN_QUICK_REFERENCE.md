# Admin Panel Quick Reference Guide

## 🔐 Login Credentials (Test Accounts - Dutch Email Format)

### Super Admin (Full System Access)
- **Email:** j.devries@area42.nl
- **Password:** SuperAdmin@123
- **Rank:** Systeembeheerder
- **Access:** Everything

### Property Management
- **Email:** e.deboer@area42.nl
- **Password:** PropertyManager@456
- **Rank:** Beheerder Accommodaties
- **Access:** Accommodations & Pricing management

### Reservation Management
- **Email:** t.vandenberg@area42.nl
- **Password:** BookingManager@789
- **Rank:** Reserveringsbeheerder
- **Access:** Reservations & booking management

### Senior Manager
- **Email:** a.lemmens@area42.nl
- **Password:** SeniorManager@321
- **Rank:** Senior Manager
- **Access:** Staff, Financial, Accommodations & Pricing

### Customer Support
- **Email:** l.jansen@area42.nl
- **Password:** Support@654
- **Rank:** Klantenondersteuning
- **Access:** Read-only booking access, support replies

### HR Manager
- **Email:** s.kramer@area42.nl
- **Password:** HRManager@987
- **Rank:** HR Manager (HR System)
- **Access:** HR System (separate from accommodations)

---

## 📊 Admin Dashboard Navigation

After login at `/admin`, you'll see:

### 1. **📊 Overzicht (Overview)**
- System health metrics
- Key performance indicators
- Revenue summary
- Security status
- Kill switch (for system-level emergencies)

### 2. **🔒 Veiligheid (Security)**
- Audit logs
- Security flags
- Admin account management
- MFA configuration

### 3. **👥 Personeelsbeheer (Staff Management)**
- Staff directory
- Permissions management
- Role assignments
- Performance tracking

### 4. **🏠 Accommodaties (Accommodations)** ⭐ NEW
- View all accommodations
- Search & filter by name, type, or status
- Edit accommodation details
- Update pricing per accommodation
- Manage availability
- Add new accommodations
- Delete accommodations

### 5. **💰 Prijzen (Pricing)** ⭐ NEW
- Global pricing management
- Seasonal rate adjustments
- Discount rules
- Special pricing events
- Bulk price updates
- Occupancy rates

### 6. **⚖️ GDPR**
- Data export requests
- Customer data deletion
- Privacy compliance logs

### 7. **💼 Financieel (Financial)**
- Revenue reports
- Financial transactions
- Refund management
- Financial approvals (requires MFA for amounts > €2000)

---

## 🏠 Accommodations Management Tab

### Features
- **📋 Search & Filter**
  - Filter by accommodation name or description
  - Filter by type (Chalet, Villa, Bungalow, Cottage, Apartment)
  - Show only active accommodations

- **👁️ View Details**
  - Accommodation name
  - Type classification
  - Max guests capacity
  - Number of bedrooms
  - Number of bathrooms
  - Active/Inactive status

- **✏️ Edit**
  - Update accommodation details (coming soon)
  - Modify pricing
  - Update availability

- **💰 Pricing**
  - Adjust price per night
  - View pricing history
  - Seasonal adjustments

- **🗑️ Delete**
  - Remove accommodation from system
  - Requires confirmation

### Search Example
- Search for "Villa" to find all villa-type properties
- Check "Alleen beschikbaar" (Active only) to see enabled properties only
- Use the search box to find by name (e.g., "Beachfront Villa 1")

---

## 💡 Tips & Best Practices

### Security
1. **Never share your credentials** - Each admin has unique login
2. **Logout when leaving** - Click "Uitloggen" (Logout) button
3. **MFA for big changes** - Financial transactions > €2000 require two-factor authentication
4. **Audit everything** - All your actions are logged and monitored ⚠️

### Accommodation Management
1. **Update prices regularly** - Keep rates competitive and reflective of demand
2. **Batch updates** - Use bulk pricing for seasonal changes
3. **Keep descriptions current** - Good descriptions improve booking rates
4. **Monitor availability** - Ensure calendar is synced with your booking system

### Financial Operations
- Transactions under €150 (support staff) or €500 (booking managers) are autonomous
- Transactions €150-€2000 need manager approval
- Transactions > €2000 need senior admin approval + MFA
- Refunds > €2000 require dual approval

---

## 🚨 Emergency Features

### Kill Switch
Located in the Overview tab:
- **Purpose:** Disable entire system for emergency maintenance/security
- **Access:** SuperAdmin and Admin only
- **Requires:** Two-step confirmation
- **Use:** Only in critical situations (security breach, data corruption, etc.)

---

## 📞 Support & Troubleshooting

### Common Issues

**Q: Can't login?**
- Verify email format: `firstinitial.lastname@area42.nl`
- Check CAPS LOCK
- Reset password if forgotten
- Contact: IT Support

**Q: Can't see Accommodations tab?**
- Your role may not have permission
- Contact your admin to upgrade permissions
- Required: PropertyManager, SeniorManager, Admin, or SuperAdmin rank

**Q: Price update not reflecting?**
- Changes may take up to 2 minutes to appear on public site
- Clear browser cache (Ctrl+F5)
- Verify you have edit permissions

**Q: Need financial approval?**
- Contact a Senior Manager for amounts €150-€2000
- Contact Admin for amounts €2000-€5000
- SuperAdmin needed for amounts > €5000

---

## 📋 Checklists

### Daily Checklist
- [ ] Check accommodation availability status
- [ ] Review new reservations
- [ ] Monitor customer support tickets
- [ ] Check financial transactions
- [ ] Review error logs

### Weekly Checklist
- [ ] Update seasonal pricing if needed
- [ ] Review revenue report
- [ ] Check accommodation occupancy rates
- [ ] Audit staff activity
- [ ] Verify all systems operational

### Monthly Checklist
- [ ] Review comprehensive financial report
- [ ] Audit all admin activities
- [ ] Check security logs
- [ ] Plan promotions/seasonal pricing
- [ ] Review customer satisfaction data

---

## 🔗 Useful Links

- **Main App:** https://area42.local
- **Admin Panel:** https://area42.local/admin
- **API Documentation:** https://api.area42.local/docs
- **Status Page:** https://area42.local/status
- **Support:** support@area42.nl

---

**Last Updated:** 2024
**Version:** 1.0 (Initial Release)
