using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CallAPITest.Models;
using CallAPITest.Services;

namespace CallAPITest.Controllers;

public class HomeController(IWeatherForecastService WeatherForecastService) : Controller
{
    private readonly IWeatherForecastService _weatherForecastService = WeatherForecastService; 

    public async Task<IActionResult> Index()
    {
        var weatherForecast = await _weatherForecastService.GeteatherForecastAsync();
        return View(weatherForecast);
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
