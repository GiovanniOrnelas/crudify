using Microsoft.OpenApi.Models;

namespace Crudify.Infrastructure.AppConfigurations
{
    public class SwaggerConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Crudify.Api", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new[] { "", "" }
                    }
                });

                options.ResolveConflictingActions(actions => actions.FirstOrDefault());
            });
        }
    }
}
