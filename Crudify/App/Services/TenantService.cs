using Crudify.Domain.Interfaces.Repository;
using Crudify.Domain.Interfaces.Services;
using Crudify.Dto.Result;

namespace Crudify.App.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;

        public TenantService(ITenantRepository tenantRepository)
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