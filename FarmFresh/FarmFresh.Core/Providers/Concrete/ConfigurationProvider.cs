using IConfigurationProvider = FarmFresh.Core.Providers.Abstract.IConfigurationProvider;

namespace FarmFresh.Core.Providers.Concrete
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        #region private variables...

        private static string currentDirectory = Directory.GetCurrentDirectory();
        private static string configurationFilesDirectory = @$"{currentDirectory}/Configurations/AppSettings";
        private IConfigurationRoot appSettingsConfigurations;

        // environment variables
        private string issuerEnvironmentVariableName = "JWT_ISSUER";
        private string authorityEnvironmentVariableName = "JWT_AUTHORITY";
        private string audienceEnvironmentVariableName = "JWT_AUDIENCE";
        private string secretKeyEnvironmentVariableName = "JWT_SECRETKEY";
        private string expiryDurationEnvironmentVariableName = "JWT_EXPIRY_DURATION_IN_MINUTES";

        // appsettings
        private string issuerAppSettingsName = "Jwt:Issuer";
        private string authorityAppSettingsName = "Jwt:Authority";
        private string audienceAppSettingsName = "Jwt:Audience";
        private string secretKeyAppSettingsName = "Jwt:SecretKey";
        private string expiryDurationAppSettingsName = "Jwt:ExpiryDurationInMinutes";

        #endregion

        public ConfigurationProvider()
        {
            appSettingsConfigurations = BuildAppSettingsConfiguration();
        }

        #region properties...

        public string EnvironmentName => GetEnvironmentName();
        public string Issuer => GetIssuer();
        public string Authority => GetAuthority();
        public string Audience => GetAudience();
        public string SecretKey => GetSecretKey();
        public double ExpiryDuration => GetExpiryDuration();

        #endregion

        #region private methods...
        private string GetIssuer()
        {
            var result = GetFromEnvironmentOrAppSettings(issuerEnvironmentVariableName,
                issuerAppSettingsName);

            return result;
        }

        private string GetAuthority()
        {
            var result = GetFromEnvironmentOrAppSettings(authorityEnvironmentVariableName,
                authorityAppSettingsName);

            return result;
        }

        private string GetAudience()
        {
            var result = GetFromEnvironmentOrAppSettings(audienceEnvironmentVariableName,
                audienceAppSettingsName);

            return result;
        }

        private string GetSecretKey()
        {
            var result = GetFromEnvironmentOrAppSettings(secretKeyEnvironmentVariableName,
                secretKeyAppSettingsName);

            return result;
        }

        private double GetExpiryDuration()
        {
            var result = GetFromEnvironmentOrAppSettings(expiryDurationEnvironmentVariableName,
                expiryDurationAppSettingsName);

            return double.Parse(result);
        }

        private string GetEnvironmentName()
        {
            string environmentName = null;

            environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT");

            if (string.IsNullOrEmpty(environmentName) || string.IsNullOrWhiteSpace(environmentName))
            {
                environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            }

            return environmentName;
        }

        private IConfigurationRoot BuildAppSettingsConfiguration()
        {
            if (string.IsNullOrEmpty(EnvironmentName) || string.IsNullOrWhiteSpace(EnvironmentName))
            {
                throw new NullReferenceException("Environment name is null or empty");
            }

            // Set configurations file
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(configurationFilesDirectory)
                .AddJsonFile($"appsettings.json", true, false)
                .AddJsonFile($"appsettings.{EnvironmentName}.json", true, false);


            // Load configurations
            return configurationBuilder.Build();
        }

        private string GetFromEnvironmentOrAppSettings(string environmetVariableName,
            string appSettingsName)
        {
            string result = null;

            result = Environment.GetEnvironmentVariable(environmetVariableName);

            if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
            {
                result = appSettingsConfigurations.GetSection(appSettingsName).Value;
            }

            return result;
        }

        #endregion

    }
}
