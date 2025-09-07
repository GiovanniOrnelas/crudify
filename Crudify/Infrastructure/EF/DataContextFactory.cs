using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Crudify.Infrastructure.EF
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder
                .UseSqlServer(configuration["ConnectionString"])
                .ConfigureWarnings(warnings =>
                    warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning,
                                    RelationalEventId.BoolWithDefaultWarning))
                .UseLazyLoadingProxies();

            return new DataContext(optionsBuilder.Options);
        }
    }
}