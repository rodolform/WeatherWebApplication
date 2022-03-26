namespace IDB_Weather.Helper
{
    public class AppState
    {
        public event Action OnChange;

        //set light or dark mode
        public float? SelectedLightMode { get; private set; } = 1;
        public void SetLightMode(float? _mode)
        {
            SelectedLightMode = _mode;
            NotifyStateChanged();
        }

        //Replicate the event
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
