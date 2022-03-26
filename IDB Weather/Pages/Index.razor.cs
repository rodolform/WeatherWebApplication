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
        protected string? CurrentLatitude => CurrentPositionResult?.Position?.Coords?.Latitude.ToString("F2");
        protected string? CurrentLongitude => CurrentPositionResult?.Position?.Coords?.Longitude.ToString("F2");

        public string lat { get; set; }
        public string lon { get; set; }

        public CurrentWeather weatherForecast { get; set; }
        public Units units { get; set; } = Units.imperial;

        /// <summary>
        /// Initialize screen
        /// </summary>
        protected override void OnInitialized()
        {
            //This will allow to cascade the selected value from NavMenu component
            AppState.OnChange += Reload;

            //This will allow to cascade the click event NavMenu component
            AppState.OnChangeLocation += ReloadLocationAndData;

            //This will allow to cascade the click event NavMenu component
            AppState.OnChangeCity += LoadCityData;
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
                await GetCurrentPositionAndRefreshDataAsync();
            }
        }

        /// <summary>
        /// Get the Current Position and Refresh Data
        /// </summary>
        /// <returns></returns>
        private async Task GetCurrentPositionAndRefreshDataAsync()
        {
            CurrentPositionResult = await GetCurrentPositionAsync();

            if (CurrentPositionResult != null && CurrentPositionResult.Position != null)
            {
                lat = CurrentLatitude;
                lon = CurrentLongitude;

                weatherForecast = await weatherClientService.GetCurrentWeatherAsync(units, "EN", lat, lon);
                StateHasChanged();
            }
        }

        /// <summary>
        /// Get City Position And Refresh Data
        /// </summary>
        /// <returns></returns>
        private async Task GetCityPositionAndRefreshDataAsync(string _city)
        {
            try
            {
                weatherForecast = await weatherClientService.GetCurrentWeatherAsync(units, "EN", null, null, _city);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Reload data
        /// </summary>
        private void Reload()
        {
            units = AppState.SelectedUnit ? Units.metric : Units.imperial;

            weatherForecast = weatherClientService.GetCurrentWeatherAsync(units, "EN", lat, lon, AppState.SelectedCity).Result;
            StateHasChanged();
        }

        /// <summary>
        /// Reload Location and Data
        /// </summary>
        private void ReloadLocationAndData()
        {
            _ = GetCurrentPositionAndRefreshDataAsync();
            StateHasChanged();
        }

        /// <summary>
        /// Load City Data
        /// </summary>
        private void LoadCityData()
        {
            _ = GetCityPositionAndRefreshDataAsync(AppState.SelectedCity);
            StateHasChanged();
        }

        /// <summary>
        /// Get the current position after giving access from browser
        /// </summary>
        /// <returns></returns>
        public async Task<GeolocationResult> GetCurrentPositionAsync()
        {
            GeolocationResult result = new GeolocationResult();
            try
            {
                result = await geolocationService.GetCurrentPosition();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
