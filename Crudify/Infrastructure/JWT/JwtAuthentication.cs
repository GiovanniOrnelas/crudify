using Crudify.Commons.Providers;
using Crudify.Domain.Entities;
using Crudify.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Crudify.Infrastructure.JWT
{
    public class JwtAuthentication
    {
        public static JwtModel Create(User user, long tenantId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettingsProvider.Configuration["JWT:SecretToken"]);

            var role = user.Type;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(ClaimTypes.Role, role.ToString() ),
                    new(ClaimTypes.Name, user.Id.ToString()),
                    new("tenantId", tenantId.ToString()),
                }),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(Convert.ToInt64(AppSettingsProvider.Configuration["JWT:DefaultExpiration"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return new JwtModel
            {
                AccessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
                RefreshToken = GenerateRefreshToken(),
                Expires = tokenDescriptor.Expires
            };
        }

        public static JwtSecurityToken Validate(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettingsProvider.Configuration["JWT:SecretToken"]);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }

        public static string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}