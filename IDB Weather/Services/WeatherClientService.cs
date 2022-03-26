using Darnton.Blazor.DeviceInterop.Geolocation;
using IDB_Weather.Data.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace IDB_Weather.Services
{
    public interface IWeatherClientService
    {
        Task<WeatherForecast> GetCurrentWeatherAsync(Units _units, string _lang);
    }

    public class WeatherClientService : IWeatherClientService
    {
        //private readonly ILogger logger;
        private readonly IConfiguration _configuration;

        public WeatherClientService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Method to get the current weather from the openwheater uri
        /// </summary>
        /// <param name="_units"></param>
        /// <param name="_lang"></param>
        /// <returns></returns>
        public async Task<WeatherForecast> GetCurrentWeatherAsync(Units _units, string _lang)
        {
            WeatherForecast weatherForecast = new WeatherForecast();

            try
            {
                using var client = new HttpClient();
                var HttpRequest = new HttpRequestMessage
                {
                    RequestUri = new Uri(_configuration["Application:openweathermapUri"]),
                    Method = HttpMethod.Get
                };

                var response = await client.SendAsync(HttpRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                    weatherForecast = await JsonSerializer.DeserializeAsync<WeatherForecast>(await response.Content.ReadAsStreamAsync(), options);

                    //Create the icon url based on the result and from app settings file
                    if (weatherForecast != null)
                    {
                        weatherForecast.iconUrl = $"{_configuration["Application:openweathericon"]}{weatherForecast.weather?.FirstOrDefault().icon}.png";
                    }

                    // logger.LogInformation($"Result of web service serialized: {serializedResult}");
                    // logger.LogInformation($"{nameof(this.GetCurrentWeather)} StatusCode {response.StatusCode} - {response.ReasonPhrase}");

                }
                else
                {
                    //logger.LogError($"Error on {nameof(this.GetCurrentWeather)} StatusCode {response.StatusCode} - {response.ReasonPhrase}");
                }
                
            }
            catch (Exception ex)
            {
                //logger.LogError($"Error on {nameof(this.GetCurrentWeather)} : {ex.Message} - {nameof(ex.InnerException)} : {ex.InnerException?.Message} - {nameof(ex.StackTrace)} : {ex.StackTrace}");
            }

            return weatherForecast;
        }
    }
}
