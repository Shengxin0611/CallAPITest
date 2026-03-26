using CallAPITest.Models;
using System.Text.Json;

namespace CallAPITest.Services;

public interface IWeatherForecastService
{
    Task<List<WeatherForecastModel>> GetWeatherForecastAsync();

    Task<bool> CreateWeatherAsync(WeatherForecastModel data);

    Task<bool> UpdateWeatherAsync(WeatherForecastModel data);

    Task<bool> DeleteWeatherAsync(string id);
}

public class WeatherForecastService(HttpClient httpClient) : IWeatherForecastService
{
    private readonly HttpClient _httpClient = httpClient;

    private static readonly JsonSerializerOptions options = new() 
    { 
        PropertyNameCaseInsensitive = true 
    };

    public async Task<List<WeatherForecastModel>> GetWeatherForecastAsync()
    {        
        var response = await _httpClient.GetAsync("");
        response.EnsureSuccessStatusCode();       
        var result = await response.Content.ReadAsStringAsync();        
        return JsonSerializer.Deserialize<List<WeatherForecastModel>>(result, options) ?? [];
    }
    
    public async Task<bool> CreateWeatherAsync(WeatherForecastModel data)
    {
        var jsonContent = JsonSerializer.Serialize(data, options);
        var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateWeatherAsync(WeatherForecastModel data)
    {
        var jsonContent = JsonSerializer.Serialize(data, options);
        var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync("", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteWeatherAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/{id}");
        return response.IsSuccessStatusCode;
    }
}