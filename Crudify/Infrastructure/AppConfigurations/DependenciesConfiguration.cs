using Crudify.App.Services;
using Crudify.Domain.Interfaces.Repository;
using Crudify.Domain.Interfaces.Services;
using Crudify.Infrastructure.EF;
using Crudify.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Crudify.Infrastructure.AppConfigurations
{
    public class DependenciesConfiguration
    {
        public static void Configure(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            #region Singleton
            serviceCollection.AddSingleton(configuration);
            #endregion

            #region Scoped
            serviceCollection.AddScoped<ICreateTokenService, CreateTokenService>();
            serviceCollection.AddScoped<ITenantService, TenantService>();
            serviceCollection.AddScoped<IUserService, UserService>();

            serviceCollection.AddScoped<ITenantRepository, TenantRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            #endregion

            serviceCollection.AddDbContext<DataContext>(options => options
                .UseSqlServer(configuration["ConnectionString"])
                .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning, RelationalEventId.BoolWithDefaultWarning))
                .UseLazyLoadingProxies()
            );
        }
    }
}