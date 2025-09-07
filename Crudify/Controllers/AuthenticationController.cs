using Crudify.Domain.Interfaces.Services;
using Crudify.Dto.Request;
using Crudify.Dto.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crudify.Controllers
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
        public async Task<AuthenticationResult> CreateAsync([FromBody] AuthenticationRequest request, CancellationToken cancellationToken = default)
        {
            return await _createTokenService.CreateAsync(request, cancellationToken);
        }
    }
}