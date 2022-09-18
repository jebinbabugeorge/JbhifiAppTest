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
        private readonly IOpenWeatherMapService _openWeatherMapService;

        public WeatherForecastController(IOpenWeatherMapService openWeatherMapService)
        {
            _openWeatherMapService = openWeatherMapService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string city, string country)
        {
            if (string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(country))
                return BadRequest("City and Country cannot be empty.");

            var description = await _openWeatherMapService.GetWeatherDetailsAsync(city, country);

            return Ok(description);
        }
    }
}
