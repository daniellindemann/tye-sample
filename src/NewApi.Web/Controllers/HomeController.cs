using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using NewApi.Web.Infrastructure;
using NewApi.Web.Models;

namespace NewApi.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeatherClient _weatherClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(WeatherClient weatherClient, ILogger<HomeController> logger)
        {
            _weatherClient = weatherClient;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> WeatherForecast()
        {
            var weatherForecastData = await _weatherClient.GetWeatherForecastAsync();
            
            return View(weatherForecastData);
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
}
