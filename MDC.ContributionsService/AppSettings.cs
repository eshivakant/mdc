namespace MDC.ContributionsService
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string[] AllowedOrigins { get; set; }
        public string GatewayAddress { get; set; }
    }
}