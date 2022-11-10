namespace FarmFresh.Core.Providers.Abstract
{
    public interface IConfigurationProvider
    {
        public string EnvironmentName { get; }
        public string Issuer { get; }
        public string Authority { get; }
        public string Audience { get; }
        public string SecretKey { get; }
        public double ExpiryDuration { get; }
        public string DatabaseConnectionString { get; }
    }
}
