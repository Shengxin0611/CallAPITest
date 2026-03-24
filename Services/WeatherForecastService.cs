using System.Text.Json;
using CallAPITest.Models;

namespace CallAPITest.Services;

// 定義介面（好處是未來方便抽換或測試）
public interface IWeatherForecastService
{
    Task<List<WeatherForecastModel>> GeteatherForecastAsync();
}

public class WeatherForecastService(HttpClient httpClient) : IWeatherForecastService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<WeatherForecastModel>> GeteatherForecastAsync()
    {
        var response = await _httpClient.GetAsync("https://localhost:5001/weatherforecast");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        
        return JsonSerializer.Deserialize<List<WeatherForecastModel>>(content, options) ?? new List<WeatherForecastModel>();
    }
}