using Crudify.Dto;

namespace Crudify.Domain.Interfaces.Services
{
    public interface ITenantService
    {
        Task<TenantResult> GetByIdAsync(long id);
    }
}