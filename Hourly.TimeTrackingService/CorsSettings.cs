namespace Hourly.TimeTrackingService
{
    public class CorsSettings
    {
        public string[] AllowedOrigins { get; set; } = [];
        public string[] AllowedMethods { get; set; } = [];
        public string[] AllowedHeaders { get; set; } = [];
        public string[] ExposedHeaders { get; set; } = [];
        public bool AllowCredentials { get; set; }
        public int MaxAge { get; set; }
    }

}
