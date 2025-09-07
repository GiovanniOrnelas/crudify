using Crudify.Commons.Enums;
using Crudify.Commons.Extensions;

namespace Crudify.Infrastructure.AppConfigurations
{
    public class AuthorizationConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            var admins = new List<string> {
                UserType.Management.GetDescription()
            };

            services.AddAuthorization(options =>
            {
                
            });
        }
    }
}
