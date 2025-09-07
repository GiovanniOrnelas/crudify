//using Crudify.Domain.Entities.User;
using Crudify.Dto.Request;
using Crudify.Dto.Result;

namespace Crudify.Domain.Interfaces.Services
{
    public interface ICreateTokenService
    {
        Task<AuthenticationResult> CreateAsync(AuthenticationRequest request, CancellationToken cancellationToken);
    }
}
