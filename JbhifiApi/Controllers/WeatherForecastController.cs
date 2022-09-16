using JbhifiApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JbhifiApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IOpenWeatherMapService _openWeatherMapService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOpenWeatherMapService openWeatherMapService)
        {
            _logger = logger;
            _openWeatherMapService = openWeatherMapService;
        }

        [HttpGet]
        public async Task<string> Get(string city, string country)
        {
            return await _openWeatherMapService.GetWeatherDetailsAsync(city, country);
        }
    }
}
