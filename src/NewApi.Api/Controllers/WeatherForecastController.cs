using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

using NewApi.Api.Models;

using Newtonsoft.Json;

namespace NewApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly string CacheKey = "weatherforecast";

        private readonly IDistributedCache _cache;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IDistributedCache cache, ILogger<WeatherForecastController> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            _logger.LogInformation("Get weather forecast");
            var weatherForeCastData = await _cache.GetStringAsync(CacheKey);

            if (!string.IsNullOrEmpty(weatherForeCastData))
            {
                _logger.LogInformation("Got weather forecast from cache");
                return JsonConvert.DeserializeObject<WeatherForecast[]>(weatherForeCastData);
            }

            _logger.LogInformation("Generate weather forecast");
            var rng = new Random();
            var items = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            _logger.LogInformation("{count} forecast items generated", items.Length);

            // add to cache
            _logger.LogInformation("Add forecast data to cache");
            await _cache.SetStringAsync(CacheKey,
                JsonConvert.SerializeObject(items),
                new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });

            _logger.LogInformation("Return forecast data");
            return items;
        }
    }
}
