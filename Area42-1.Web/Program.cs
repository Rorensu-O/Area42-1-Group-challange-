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

builder.Services.AddOutputCache();

// Configure HTTP client for Area42 API
// AddServiceDefaults() already includes service discovery via HttpClient defaults
builder.Services.AddHttpClient("Area42API", client =>
{
    // When Aspire resolves this, it will use the service name "apiservice"
    // In local development, appsettings.Development.json will override this
    var apiUrl = builder.Configuration["ApiUrl"] ?? "http://apiservice";
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
