using IDB.Weather.Model.ModelForecast;
using IDB.Weather.Model.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace IDB.UnitTest
{
    internal class WeatherClientService
    {
        /// <summary>
        /// Method to get the current weather from the openwheater uri
        /// </summary>
        /// <param name="_units"></param>
        /// <param name="_lang"></param>
        /// <returns></returns>
        public async Task<CurrentWeather> GetCurrentWeatherAsync(Units _units, string _lang, string _lat, string _lon, string _city = null)
        {
            CurrentWeather weatherForecast = new CurrentWeather();

            try
            {
                if (!string.IsNullOrEmpty(_city))
                {
                    _lat = null;
                    _lon = null;
                }
                string prefix = "https://api.openweathermap.org/data/2.5/weather?appid=";
                using var client = new HttpClient();

                string uri = $"{prefix}d304084249f663adec55daece137529f";

                if (!string.IsNullOrEmpty(_lat) && !string.IsNullOrEmpty(_lon))
                {
                    uri = uri + $"&lat={_lat}&lon={_lon}";
                }

                if (!string.IsNullOrEmpty(_city))
                {
                    uri = uri + $"&q={_city}";
                }

                uri = uri + $"&units={_units}";

                var HttpRequest = new HttpRequestMessage
                {
                    RequestUri = new Uri(uri),
                    Method = HttpMethod.Get
                };

                var response = await client.SendAsync(HttpRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                    weatherForecast = await JsonSerializer.DeserializeAsync<CurrentWeather>(await response.Content.ReadAsStreamAsync(), options);

                    //Create the icon url based on the result and from app settings file
                    if (weatherForecast != null)
                    {
                        weatherForecast.iconUrl = $"openweathericon{weatherForecast.weather?.FirstOrDefault().icon}.png";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on {nameof(this.GetCurrentWeatherAsync)} : {ex.Message} - {nameof(ex.InnerException)} : {ex.InnerException?.Message} - {nameof(ex.StackTrace)} : {ex.StackTrace}");
            }

            return weatherForecast;
        }

        /// <summary>
        /// Method to get the current weather from the openwheater uri 5 days
        /// </summary>
        /// <param name="_units"></param>
        /// <param name="_lang"></param>
        /// <param name="_lat"></param>
        /// <param name="_lon"></param>
        /// <param name="_city"></param>
        /// <returns></returns>
        public async Task<WeatherForecast> GetForecast5DaysWeatherAsync(Units _units, string _lang, string _lat, string _lon, string _city = null)
        {
            WeatherForecast weatherForecast = new WeatherForecast();

            try
            {
                string prefix = "https://api.openweathermap.org/data/2.5/forecast?appid=";
                using var client = new HttpClient();

                string uri = $"{prefix}d304084249f663adec55daece137529f";

                if (!string.IsNullOrEmpty(_lat) && !string.IsNullOrEmpty(_lon))
                {
                    uri = uri + $"&lat={_lat}&lon={_lon}";
                }

                if (!string.IsNullOrEmpty(_city))
                {
                    uri = uri + $"&q={_city}";
                }

                uri = uri + $"&units={_units}";

                var HttpRequest = new HttpRequestMessage
                {
                    RequestUri = new Uri(uri),
                    Method = HttpMethod.Get
                };

                var response = await client.SendAsync(HttpRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                    weatherForecast = await JsonSerializer.DeserializeAsync<WeatherForecast>(await response.Content.ReadAsStreamAsync(), options);

                    //Create the icon url based on the result and from app settings file
                    if (weatherForecast != null && weatherForecast.list != null)
                    {
                        foreach (var item in weatherForecast.list)
                        {
                            item.iconUrl = $"openweathericon{item.weather?.FirstOrDefault().icon}.png";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on {nameof(this.GetForecast5DaysWeatherAsync)} : {ex.Message} - {nameof(ex.InnerException)} : {ex.InnerException?.Message} - {nameof(ex.StackTrace)}");
            }

            return weatherForecast;
        }

    }
}
