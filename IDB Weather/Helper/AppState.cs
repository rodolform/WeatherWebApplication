﻿namespace IDB_Weather.Helper
{
    public class AppState
    {
        public event Action OnChange;
        public event Action OnChangeLocation;

        //set light or dark mode
        public float? SelectedLightMode { get; private set; } = 1;
        public void SetLightMode(float? _mode)
        {
            SelectedLightMode = _mode;
            NotifyStateChanged();
        }

        //set city
        public string? SelectedCity { get; private set; }
        public void SetCity(string? _city)
        {
            SelectedCity = _city;
            NotifyStateChanged();
        }

        //set unit
        public bool SelectedUnit { get; private set; }
        public void SetUnit(bool _unit)
        {
            SelectedUnit = _unit;
            NotifyStateChanged();
        }

        //Set CurrentLocation        
        public void SetCurrentLocation()
        {
            NotifyStateChangedLocation();
        }

        //Replicate the event
        private void NotifyStateChanged() => OnChange?.Invoke();

        //Replicate the event for location
        private void NotifyStateChangedLocation() => OnChangeLocation?.Invoke();
    }
}
