using Crudify.Domain.Interfaces.Services.Authentication;
using Crudify.Dto.Authentication;
using Crudify.Dto.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crudify.Controllers.Authentication
{
    [Route("api/v1/authentication")]
    public class AuthenticationController : BaseController
    {
        private readonly ICreateTokenService _createTokenService;
        public AuthenticationController(ICreateTokenService createTokenService)
        {
            _createTokenService = createTokenService;
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<AuthResponse> CreateAsync([FromBody] AuthenticationRequest request, CancellationToken cancellationToken = default)
        {
            return await _createTokenService.CreateAsync(request, cancellationToken);
        }
    }
}