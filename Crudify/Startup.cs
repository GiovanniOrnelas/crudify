using Crudify.App;
using Crudify.Commons.Providers;
using Crudify.Infrastructure.AppConfigurations;
using Microsoft.AspNetCore.HttpOverrides;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crudify
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration _configuration)
        {
            Configuration = _configuration;
            AppSettingsProvider.Configuration = Configuration;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseRouting();

            app.UseCors("AllowCors");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crudify.Api v1");
                c.RoutePrefix = "swagger";
            });

            app.UseGlobalExceptionHandler(loggerFactory);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options => {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor;
            });

            services.AddCors(options => options.AddPolicy("AllowCors", builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));

            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = Configuration["Redis"];
            //    options.InstanceName = Configuration["RedisInstance"];
            //});

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            SwaggerConfiguration.Configure(services);
            AuthenticationConfiguration.Configure(services, Configuration);
            DependenciesConfiguration.Configure(services, Configuration);
            AuthorizationConfiguration.Configure(services);

            services.AddHttpClient();
            services.AddControllers();
            services.AddResponseCaching();
            services.AddResponseCompression();
        }
    }
}