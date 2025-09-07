using Microsoft.Extensions.Configuration;

namespace Crudify.Commons.Providers
{
    public static class AppSettingsProvider
    {
        public static IConfiguration Configuration { get; set; }
    }
}
