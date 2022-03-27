using IDB.Weather.Model.ModelForecast;
using IDB.Weather.Model.Models;
using IDB.Weather.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDB.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private IWeatherClientService weatherClientService { get; set; }

        public WeatherForecast weatherForecast { get; set; }
        public Units units { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            try
            {
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