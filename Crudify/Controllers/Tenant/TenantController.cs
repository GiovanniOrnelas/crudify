using Crudify.App.Services.Tenant;
using Crudify.Domain.Interfaces.Services;
using Crudify.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crudify.Controllers.Tenant
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