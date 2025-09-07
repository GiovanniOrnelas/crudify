using Crudify.App.Services;
using Crudify.Domain.Entities;
using Crudify.Domain.Interfaces.Repository;
using Moq;

namespace Crudify.Test.App.Services
{
    public class TenantServiceTest
    {
        [Fact]
        public async Task GetByIdAsync_ShouldReturnTenantWithSameId()
        {
            var tenantId = 1;
            var tenant = new Tenant("Crudify") { Id = tenantId };

            var repoMock = new Mock<ITenantRepository>();
            repoMock.Setup(r => r.GetByIdAsync(tenantId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(tenant);

            var service = new TenantService(repoMock.Object);

            var result = await service.GetByIdAsync(tenantId);

            Assert.NotNull(result);
            Assert.Equal(tenantId, result.Id);
            Assert.Equal(tenant.Name, result.Name);
        }
    }
}