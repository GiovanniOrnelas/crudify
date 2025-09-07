using Crudify.Domain.Interfaces.Services.Authentication;
using Crudify.Dto.Authentication;
using Crudify.Dto.JWT;
using Crudify.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Crudify.App.Services.Authentication
{
    public class CreateTokenService : ICreateTokenService
    {
        //private readonly LoginControlRepository _loginControlRepository;
        private readonly DataContext _context;

        public CreateTokenService(/*LoginControlRepository loginControlRepository*/ DataContext context)
        {
            //_loginControlRepository = loginControlRepository;
            _context = context;
        }

        public async Task<AuthResponse> CreateAsync(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == request.Email && x.Password == request.Password, cancellationToken);

            if (user == null)
                throw new ApplicationException("Usuário não existente.");

            var auth = new AuthResponse(user, user.TenantId);

            //var loginControl = await _loginControlRepository
            //    .SaveAsync(new LoginControl(user.Id, user.TenantId, fromRefresh, fromThirdPartyService, auth.Token.Expires));

            //auth.LoginControlId = loginControl?.Id ?? default;
            //auth.LoggedAt = loginControl?.RequestedAt ?? default;

            //auth.FromRefresh = fromRefresh;
            //auth.FromThird = fromThirdPartyService;
            //auth.DepartmentId = user?.Athlete?.DepartmentId ?? default;
            //auth.Gender = user?.Gender ?? default;

            return auth;
        }
    }
}