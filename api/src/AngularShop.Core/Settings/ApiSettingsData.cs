namespace AngularShop.Core.Settings
{
    public class ApiSettingsData
    {
        public string ApiUrl { get; set; }
        public string ApiOriginUrl { get; set; }
        public TokenApiSettingsData Token { get; set; }
    }

    public class TokenApiSettingsData
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}