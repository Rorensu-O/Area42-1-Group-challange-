using Area42_1.Web;
using Area42_1.Web.Components;
using Area42_1.Web.Components.Services;
using Area42_1.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AppUiState>();
builder.Services.AddHttpClient<DutchAddressLookup>();
builder.Services.AddScoped<DutchAddressRepository>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AccountValidationService>();
builder.Services.AddScoped<AdminAuthorizationService>();
builder.Services.AddScoped<KillSwitchService>();
builder.Services.AddScoped<INotificationService, ConsoleNotificationService>();

// Admin API service for backend integration
builder.Services.AddHttpClient<AdminApiService>(client =>
{
    var apiUrl = builder.Configuration["ApiUrl"] ?? "https://localhost:7001";
    client.BaseAddress = new Uri(apiUrl);
});

builder.Services.AddOutputCache();

// HTTP client for API communication
builder.Services.AddHttpClient("Area42API", client =>
{
    var apiUrl = builder.Configuration["ApiUrl"] ?? "https://localhost:7001";
    client.BaseAddress = new Uri(apiUrl);
});

// Add session for auth token storage
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(24);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add authentication state provider (placeholder)
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAntiforgery();

app.UseOutputCache();

app.UseSession();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapDefaultEndpoints();

app.Run();
