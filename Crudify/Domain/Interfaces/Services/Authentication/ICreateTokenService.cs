//using Crudify.Domain.Entities.User;
using Crudify.Dto.Authentication;
using Crudify.Dto.JWT;

namespace Crudify.Domain.Interfaces.Services.Authentication
{
    public interface ICreateTokenService
    {
        Task<AuthResponse> CreateAsync(AuthenticationRequest request, CancellationToken cancellationToken);
    }
}
