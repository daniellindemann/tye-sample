using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using NewApi.Web.Models;

using Newtonsoft.Json;

namespace NewApi.Web.Infrastructure
{
    public class WeatherClient
    {
        private readonly HttpClient _httpClient;

        public WeatherClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<WeatherForecast>> GetWeatherForecastAsync()
        {
            var response = await _httpClient.GetAsync("/api/weatherforecast");
            var responseData = await response.Content.ReadAsStringAsync();
            var forecastData = JsonConvert.DeserializeObject<List<WeatherForecast>>(responseData);
            return forecastData;
        }
    }
}