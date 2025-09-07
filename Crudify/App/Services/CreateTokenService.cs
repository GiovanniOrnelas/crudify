using Crudify.Domain.Interfaces.Services;
using Crudify.Dto.Request;
using Crudify.Dto.Result;
using Crudify.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Crudify.App.Services
{
    public class CreateTokenService : ICreateTokenService
    {
        private readonly DataContext _context;

        public CreateTokenService(DataContext context)
        {
            _context = context;
        }

        public async Task<AuthenticationResult> CreateAsync(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == request.Email && x.Password == request.Password, cancellationToken);

            if (user == null)
                throw new ApplicationException("User doesn't exist");

            var auth = new AuthenticationResult(user, user.TenantId);

            return auth;
        }
    }
}