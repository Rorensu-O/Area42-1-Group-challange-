namespace Area42_1.Web.Services;

using System.Text.Json;

public class AccommodationApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AccommodationApiClient> _logger;

    public AccommodationApiClient(IHttpClientFactory httpClientFactory, ILogger<AccommodationApiClient> logger)
    {
        _httpClient = httpClientFactory.CreateClient("Area42API");
        _logger = logger;
    }

    public async Task<List<AccommodationDto>?> GetAllAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<AccommodationDto>>("/api/accommodations");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all accommodations");
            return null;
        }
    }

    public async Task<AccommodationDto?> GetByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<AccommodationDto>($"/api/accommodations/{id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching accommodation {Id}", id);
            return null;
        }
    }

    public async Task<List<AccommodationDto>?> GetByTypeAsync(string type)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<AccommodationDto>>($"/api/accommodations/type/{type}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching accommodations by type {Type}", type);
            return null;
        }
    }

    public async Task<AccommodationDto?> CreateAsync(CreateAccommodationRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/accommodations", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AccommodationDto>();
            }
            _logger.LogWarning("Failed to create accommodation: {StatusCode}", response.StatusCode);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating accommodation");
            return null;
        }
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateAccommodationRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/accommodations/{id}", request);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Successfully updated accommodation {Id}", id);
                return true;
            }
            _logger.LogWarning("Failed to update accommodation {Id}: {StatusCode}", id, response.StatusCode);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating accommodation {Id}", id);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/api/accommodations/{id}");
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Successfully deleted accommodation {Id}", id);
                return true;
            }
            _logger.LogWarning("Failed to delete accommodation {Id}: {StatusCode}", id, response.StatusCode);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting accommodation {Id}", id);
            return false;
        }
    }

    public async Task<bool> UpdatePricingAsync(Guid id, decimal pricePerNight)
    {
        try
        {
            var request = new { pricePerNight };
            var response = await _httpClient.PatchAsJsonAsync($"/api/accommodations/{id}/pricing", request);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Successfully updated pricing for accommodation {Id}", id);
                return true;
            }
            _logger.LogWarning("Failed to update pricing for accommodation {Id}: {StatusCode}", id, response.StatusCode);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating pricing for accommodation {Id}", id);
            return false;
        }
    }
}


public class CreateAccommodationRequest
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MaxGuests { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}

public class UpdateAccommodationRequest
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }
    public int? MaxGuests { get; set; }
    public int? Bedrooms { get; set; }
    public int? Bathrooms { get; set; }
    public string? ImageUrl { get; set; }
    public bool? IsActive { get; set; }
}

public class AccommodationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int MaxGuests { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
