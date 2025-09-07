using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace Crudify.Infrastructure.AppConfigurations
{
    public class AuthenticationConfiguration
    {
        private static HashSet<string> Blocked = new HashSet<string>();

        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretToken"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                x.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = context =>
                    {
                        if (context.SecurityToken is System.IdentityModel.Tokens.Jwt.JwtSecurityToken)
                        {
                            var tenantId = ((System.IdentityModel.Tokens.Jwt.JwtSecurityToken)context.SecurityToken)?.Claims?.FirstOrDefault(d => d.Type.Equals("tenantId"))?.Value;
                            if (Blocked.Contains(tenantId))
                            {
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                context.Response.ContentType = "application/json";

                                return context.Response.WriteAsync(JsonSerializer.Serialize(new
                                {
                                    error = "FORBIDDEN"
                                }));
                            }
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}