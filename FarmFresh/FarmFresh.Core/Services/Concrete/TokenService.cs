using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IConfigurationProvider = FarmFresh.Core.Providers.Abstract.IConfigurationProvider;

namespace FarmFresh.Core.Services.Concrete
{
    public class TokenService : ITokenService
    {
        private double expiryDurationMinutes;
        private IConfigurationProvider configurationProvider;
        private string issuer;
        private string secretKey;
        private string audience;

        public TokenService(IConfigurationProvider configurationProvider)
        {
            this.configurationProvider = configurationProvider;
            issuer = configurationProvider.Issuer;
            secretKey = configurationProvider.SecretKey;
            audience = configurationProvider.Audience;
            expiryDurationMinutes = configurationProvider.ExpiryDuration;
        }

        public string BuildToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new JwtSecurityToken(issuer,
                audience,
                null,
                expires: DateTime.Now.AddMinutes(expiryDurationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public (string, double) BuildToken(Claim[] claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new JwtSecurityToken(issuer,
                audience,
                claims,
                expires: DateTime.Now.AddMinutes(expiryDurationMinutes),
                signingCredentials: credentials);

            return (new JwtSecurityTokenHandler().WriteToken(tokenDescriptor), expiryDurationMinutes);
        }

        public bool IsValidToken(string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(secretKey);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = mySecurityKey,
                    },
                    out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }

}
