using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Area42_1.Web;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IHttpClientFactory _httpClientFactory;
    private ISession? _session;

    public CustomAuthStateProvider(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = GetTokenFromStorage();

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var claims = ParseClaimsFromToken(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            var principal = new ClaimsPrincipal(identity);

            return new AuthenticationState(principal);
        }
        catch
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    public async Task LoginAsync(string token)
    {
        SaveTokenToStorage(token);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutAsync()
    {
        RemoveTokenFromStorage();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private string? GetTokenFromStorage()
    {
        // In a real application, you would store this in localStorage via JS interop
        return null;
    }

    private void SaveTokenToStorage(string token)
    {
        // In a real application, you would store this in localStorage via JS interop
    }

    private void RemoveTokenFromStorage()
    {
        // In a real application, you would remove from localStorage via JS interop
    }

    private List<Claim> ParseClaimsFromToken(string token)
    {
        // Simplified claim parsing - in production use a proper JWT library
        var claims = new List<Claim>();

        try
        {
            var parts = token.Split('.');
            if (parts.Length == 3)
            {
                var payload = parts[1];
                // Add padding if needed
                var padding = 4 - (payload.Length % 4);
                if (padding != 4)
                    payload += new string('=', padding);

                var decodedBytes = Convert.FromBase64String(payload);
                var json = System.Text.Encoding.UTF8.GetString(decodedBytes);

                // Parse basic claims - in production use JsonSerializer
                if (json.Contains("sub"))
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, ExtractJsonValue(json, "sub")));
                if (json.Contains("email"))
                    claims.Add(new Claim(ClaimTypes.Email, ExtractJsonValue(json, "email")));
                if (json.Contains("role"))
                    claims.Add(new Claim(ClaimTypes.Role, ExtractJsonValue(json, "role")));
            }
        }
        catch
        {
            // Token parsing failed
        }

        return claims;
    }

    private string ExtractJsonValue(string json, string key)
    {
        var startIndex = json.IndexOf($"\"{key}\":\"") + key.Length + 4;
        var endIndex = json.IndexOf("\"", startIndex);
        return json.Substring(startIndex, endIndex - startIndex);
    }
}
