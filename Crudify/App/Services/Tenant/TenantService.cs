using Crudify.Domain.Interfaces.Services;
using Crudify.Dto;
using Crudify.Infrastructure.Repository;

namespace Crudify.App.Services.Tenant
{
    public class TenantService : ITenantService
    {
        private readonly TenantRepository _tenantRepository;

        public TenantService(TenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }


        public async Task<TenantResult> GetByIdAsync(long id)
        {
            var tenant = await _tenantRepository.GetByIdAsync(id);
            return (TenantResult)tenant;
        }
    }
}