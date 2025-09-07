using Crudify.Domain.Interfaces.Services;
using Crudify.Dto.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crudify.Controllers
{
    [Route("api/v1/tenant")]
    public class TenantController : BaseController
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet()]
        [Authorize]
        public async Task<TenantResult> GetByIdAsync() => await _tenantService.GetByIdAsync(UserTenant());
    }
}