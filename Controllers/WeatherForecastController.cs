using CircuitBreaker.ApiModels;
using CircuitBreaker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CircuitBreaker.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ISalesbuzzService _salesbuzzService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ISalesbuzzService salesbuzzService)
    {
        _logger = logger;
        _salesbuzzService = salesbuzzService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<List<GetQuantityResponse>> Get()
    {
        return await _salesbuzzService.GetQuantities();
    }
}
