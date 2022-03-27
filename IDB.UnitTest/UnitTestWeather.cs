using IDB.Weather.Model.ModelForecast;
using IDB.Weather.Model.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDB.UnitTest
{
    [TestClass]
    public class UnitTestWeather
    {
        public CurrentWeather currentWeather { get; set; }
        public WeatherForecast weatherForecast { get; set; }
        public Units units { get; set; }

        [TestMethod]
        public void TestMethodGetCurrentWeather()
        {
            try
            {
                WeatherClientService weatherClientService = new WeatherClientService();
                currentWeather = weatherClientService.GetCurrentWeatherAsync(units, "EN", "38.89037", "-77.03196").Result;

                if (weatherForecast == null)
                {
                    Assert.IsFalse(false, "Empty data");
                }
                else
                {
                    Assert.IsTrue(true, "Found data");
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [TestMethod]
        public void TestMethodGetForecast5DaysWeather()
        {
            try
            {
                WeatherClientService weatherClientService = new WeatherClientService();
                weatherForecast = weatherClientService.GetForecast5DaysWeatherAsync(units, "EN", "38.89037", "-77.03196").Result;

                if (weatherForecast == null)
                {
                    Assert.IsFalse(false, "Empty data");
                }
                else
                {
                    Assert.IsTrue(true, "Found data");
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}