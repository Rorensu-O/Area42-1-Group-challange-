# 🧪 Unified Login Testing Guide

## Quick Start

### Test Environment
- **Project:** Area42-1 (.NET 10 Blazor)
- **Build Status:** ✅ Successful
- **Git Branch:** `admin-login-test`

---

## Test Scenarios

### 1. Unauthenticated User - Home Page
**Steps:**
1. Open browser
2. Navigate to `https://localhost:5173/` (or dev server URL)
3. Click "Home" or navigate to `/`

**Expected Result:**
- See Area42 homepage
- See "Welkom bij Area42!" card
- Login and Register buttons visible
- Browse accommodations and other pages accessible

---

### 2. Admin Login - Happy Path
**Setup:**
- Ensure API is running
- Ensure mock data is seeded

**Steps:**
1. Navigate to `/login`
2. **Email:** `j.devries@area42.nl`
3. **Password:** `password` (or configured test password)
4. Click "Inloggen" button

**Expected Result:**
- ✅ "Admin-account detected" message shown
- ✅ Redirects to `/admin` or `/admin-dashboard`
- ✅ Admin panel loads
- ✅ Dashboard tabs visible (Overview, Accommodations, Pricing, etc.)

**Verify Admin Features:**
- Accommodations Management tab
- Pricing Management tab
- Reservation Management tab
- Customer Management tab
- Admin only sections visible

---

### 3. Customer Login - Happy Path
**Setup:**
- Customer account in database
- Can use any non-@area42.nl email

**Steps:**
1. Navigate to `/login`
2. **Email:** `customer@example.com` (or seeded customer email)
3. **Password:** `password`
4. Click "Inloggen" button

**Expected Result:**
- ✅ "Customer account detected" message shown
- ✅ Redirects to `/reservations`
- ✅ Customer reservations page loads
- ✅ Shows active and past reservations

---

### 4. Invalid Credentials
**Steps:**
1. Navigate to `/login`
2. **Email:** `test@area42.nl`
3. **Password:** `wrongpassword`
4. Click "Inloggen" button

**Expected Result:**
- ❌ Error message displayed
- ❌ User stays on login page
- ❌ No redirect occurs
- ❌ Error text shows: "Invalid credentials" or similar

---

### 5. Missing Email
**Steps:**
1. Navigate to `/login`
2. Leave email empty
3. **Password:** `somepassword`
4. Click "Inloggen" button

**Expected Result:**
- ❌ Error message below email field
- ❌ Email field highlighted with red border
- ❌ Form not submitted

---

### 6. Missing Password
**Steps:**
1. Navigate to `/login`
2. **Email:** `j.devries@area42.nl`
3. Leave password empty
4. Click "Inloggen" button

**Expected Result:**
- ❌ Error message below password field
- ❌ Password field highlighted with red border
- ❌ Form not submitted

---

### 7. Authenticated User - Home Redirect (Admin)
**Prerequisites:**
- Already logged in as admin (`j.devries@area42.nl`)

**Steps:**
1. Navigate to `/` (home page)

**Expected Result:**
- ⏳ Brief loading indicator
- ✅ Automatically redirects to `/admin`
- ✅ No manual navigation needed

---

### 8. Authenticated User - Home Redirect (Customer)
**Prerequisites:**
- Already logged in as customer

**Steps:**
1. Navigate to `/` (home page)

**Expected Result:**
- ⏳ Brief loading indicator
- ✅ Automatically redirects to `/reservations`
- ✅ No manual navigation needed

---

### 9. Logout - Admin
**Prerequisites:**
- Logged in as admin

**Steps:**
1. Go to admin dashboard
2. Click "Logout" button (typically top-right)

**Expected Result:**
- ✅ User logged out
- ✅ Redirects to `/login` or home
- ✅ localStorage cleared
- ✅ Admin flag removed

---

### 10. Logout - Customer
**Prerequisites:**
- Logged in as customer

**Steps:**
1. Go to reservations or customer area
2. Click logout/profile menu
3. Click "Logout"

**Expected Result:**
- ✅ User logged out
- ✅ Redirects to home or login
- ✅ Session cleared
- ✅ Cannot access `/reservations` without re-login

---

### 11. URL Access Control - Admin
**Prerequisites:**
- Logged in as customer

**Steps:**
1. Try to navigate directly to `/admin`

**Expected Result:**
- ❌ Access denied or redirected
- ❌ Cannot access admin dashboard
- ❌ May show "Access Denied" or authorization error

---

### 12. URL Access Control - Customer
**Prerequisites:**
- Logged in as admin

**Steps:**
1. Try to navigate directly to `/reservations`

**Expected Result:**
- ✅ May load customer page (depends on implementation)
- OR ❌ Redirects back to admin dashboard

---

## Mock Test Accounts

### Admin Accounts (All @area42.nl domain)
| Email | Name | Role |
|-------|------|------|
| j.devries@area42.nl | Jan de Vries | Admin |
| m.garcia@area42.nl | Maria Garcia | Admin |
| p.muller@area42.nl | Peter Müller | Admin |
| a.lemmens@area42.nl | Anna Lemmens | Admin |
| e.deboer@area42.nl | Emma de Boer | Admin |

### Customer Account
| Email | Password |
|-------|----------|
| customer@example.com | password |
| john.doe@gmail.com | password |

---

## Browser Developer Tools Checks

### LocalStorage
**Open DevTools** → Application/Storage → Local Storage

**Should contain after login:**
```
auth_token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
is_admin: "true" (for admin users)
user_rank: "Senior" (if applicable)
```

**Should be cleared after logout**

### Console
**Open DevTools** → Console

**Look for:**
- ✅ No 401/403 errors
- ✅ No auth state errors
- ✅ Successful network requests to `/api/auth/login`
- ⚠️ Debug messages for auth state changes

### Network Tab
**Open DevTools** → Network

**Monitor login request:**
1. Click login button
2. Look for `POST /api/auth/login`
3. Verify response:
   ```json
   {
     "success": true,
     "token": "eyJ...",
     "isAdmin": true,
     "userRank": "Junior Admin",
     "user": { ... }
   }
   ```

---

## Troubleshooting

### Issue: Login button doesn't work
**Check:**
- [ ] API is running
- [ ] `/api/auth/login` endpoint is accessible
- [ ] Network tab shows request/response
- [ ] Check browser console for errors

### Issue: User stays on login page after successful auth
**Check:**
- [ ] JWT token is valid
- [ ] localStorage is working
- [ ] CustomAuthStateProvider is properly registered in Program.cs
- [ ] Check console for parsing errors

### Issue: Redirect loop
**Check:**
- [ ] Home page logic
- [ ] Check if user is detected as authenticated
- [ ] Verify admin flag is correctly set

### Issue: "Access Denied" on admin page
**Check:**
- [ ] Email ends with @area42.nl
- [ ] JWT claims include admin indicators
- [ ] Check AdminAuthorizationService permissions

---

## Performance Checks

### Page Load Time
- Home page: < 2s
- Login page: < 1s
- Admin dashboard: < 3s
- Redirects: < 1s (after auth check)

### localStorage Size
- Should be < 5KB
- Token typical size: 2-3KB

---

## Regression Tests

### Existing Features Still Work
- [ ] Browse accommodations
- [ ] View details
- [ ] Register new account
- [ ] View pricing
- [ ] Admin can manage accommodations
- [ ] Admin can view reservations

---

## Success Criteria Checklist

- ✅ Build compiles without errors
- ✅ Login page displays correctly
- ✅ Admin detects @area42.nl email
- ✅ Customer detected for other emails
- ✅ Redirects to /admin for admin
- ✅ Redirects to /reservations for customer
- ✅ Home page redirects authenticated users
- ✅ Logout clears session
- ✅ Invalid credentials show error
- ✅ localStorage properly managed
- ✅ JWT claims properly parsed
- ✅ Authorization checks work
- ✅ No console errors
- ✅ No network errors

---

## Reporting Issues

If you find a problem, please report with:
1. Steps to reproduce
2. Expected vs actual result
3. Browser console errors (if any)
4. Network tab request/response
5. User account type (admin/customer)
6. Exact URL when issue occurs

---

## Test Completion

Once all tests pass:
1. ✅ Mark test scenarios as PASSED
2. ✅ Document any deviations
3. ✅ Note any performance observations
4. ✅ Ready for merge to main branch

**Test Date:** _______________
**Tester Name:** _______________
**All Tests Passed:** ☐ Yes  ☐ No  ☐ Minor Issues

---

## Additional Resources

- Backend API docs: See `UNIFIED_LOGIN_DOCUMENTATION.md`
- Admin features: See `ADMIN_PANEL_DOCUMENTATION.md`
- Setup guide: See `TEAM_SETUP_GUIDE.md`

