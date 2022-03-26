namespace IDB_Weather.Helper
{
    public static class UTCTimeConverter
    {
        public static string ConvertTime(double timestamp)
        {
            DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(timestamp);

            return dateTime.ToLocalTime().ToString("T");
        }
    }
}
