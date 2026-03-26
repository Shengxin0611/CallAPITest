using CallAPITest.Models;
using CallAPITest.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CallAPITest.Controllers;

public class HomeController(IWeatherForecastService WeatherForecastService) : Controller
{
    private readonly IWeatherForecastService _weatherForecastService = WeatherForecastService;

    public async Task<IActionResult> Index()
    {
        var weatherForecast = await _weatherForecastService.GetWeatherForecastAsync();
        return View(weatherForecast);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WeatherForecastModel data)
    {
        if (data == null) return BadRequest();

        var success = await _weatherForecastService.CreateWeatherAsync(data);

        if (success)
        {
            return Ok();
        }
        return StatusCode(500);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] WeatherForecastModel data)
    {
        if (data == null) return BadRequest();

        var success = await _weatherForecastService.UpdateWeatherAsync(data);

        if (success)
        {
            return Ok();
        }
        return StatusCode(500);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] WeatherForecastModel data)
    {
        // 檢查 data 是否為 null 或 ID 是否有效
        if (data == null || string.IsNullOrEmpty(data.Id))
            return BadRequest("無效的 ID");

        // 呼叫 Service
        var success = await _weatherForecastService.DeleteWeatherAsync(data.Id);

        if (success)
        {
            return Ok();
        }

        return StatusCode(500, "刪除失敗，請稍後再試。");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
