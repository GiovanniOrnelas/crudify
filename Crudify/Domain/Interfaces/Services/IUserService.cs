using Crudify.Dto.Result;

namespace Crudify.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResult> GetByIdAsync(long id);
    }
}
