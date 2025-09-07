using Crudify.App.Services.Tenant;
using Crudify.Domain.Entities;
using Crudify.Infrastructure.EF;
using Crudify.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Crudify.Test.App.Services
{
    public class TenantServiceTest
    {
        private DataContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TenantServiceTestDb")
                .Options;

            return new DataContext(options);
        }

        private TenantService GetService(DataContext context)
        {
            var repository = new TenantRepository(context);
            return new TenantService(repository);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnTenantWithSameId()
        {
            var context = GetInMemoryDbContext();

            context.Add(new Tenant("Crudify"));
            await context.SaveChangesAsync();

            var tenant = context.Tenants.First();

            var service = GetService(context);

            var result = await service.GetByIdAsync(tenant.Id);

            Assert.NotNull(result);
            Assert.Equal(tenant.Id, result.Id);
            Assert.Equal(tenant.Name.ToLower(), result.Name.ToLower());
        }
    }
}