using Blazor.Page;
using Darnton.Blazor.DeviceInterop.Geolocation;
using IDB_Weather.Data.Models;
using IDB_Weather.Helper;
using IDB_Weather.Services;
using Microsoft.AspNetCore.Components;

namespace IDB_Weather.Pages
{
    public class IndexRazor : Page
    {
        //Declare the interfaces using Dependency Injection
        [Inject] private IGeolocationService geolocationService { get; set; }
        [Inject] private IWeatherClientService weatherClientService { get; set; }
        [Inject] private AppState AppState { get; set; }

        public GeolocationResult CurrentPositionResult { get; set; }
        public GeolocationCoordinates CurentGeolocationCoordinates { get; set; }

        protected string? CurrentLatitude => CurrentPositionResult?.Position?.Coords?.Latitude.ToString("F2");
        protected string? CurrentLongitude => CurrentPositionResult?.Position?.Coords?.Longitude.ToString("F2");

        public string lat { get; set; } = "38.89037";
        public string lon { get; set; } = "-77.03196";

        public WeatherForecast weatherForecast { get; set; }
        public Units units { get; set; } = Units.imperial;

        protected override void OnInitialized()
        {
            //This will allos to cascade tje selected value from NavMenu component
            AppState.OnChange += Reload;
        }

        private void Reload()
        {
            units = AppState.SelectedUnit ? Units.metric : Units.imperial; 

            weatherForecast = weatherClientService.GetCurrentWeatherAsync(units, "EN", lat, lon).Result;
            StateHasChanged();
        }

        /// <summary>
        /// Load the data after rendering the page
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                weatherForecast = await weatherClientService.GetCurrentWeatherAsync(units, "EN", lat, lon);
                StateHasChanged();
            }
        }

        /// <summary>
        /// Get the current position after giving access from browser
        /// </summary>
        /// <returns></returns>
        public async Task GetCurrentPositionAsync()
        {
            try
            {
                CurrentPositionResult = await geolocationService.GetCurrentPosition();

                if (CurrentPositionResult != null && CurrentPositionResult.Position != null)
                {
                    CurentGeolocationCoordinates = CurrentPositionResult.Position.Coords;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Get the current weather
        /// </summary>
        /// <returns></returns>
        public async Task GetCurrentWeatherAsync()
        {
            var result = await weatherClientService.GetCurrentWeatherAsync(units, "EN", lat, lon);

            var res = result;
        }
    }
}
