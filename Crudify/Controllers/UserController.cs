using Crudify.Domain.Interfaces.Services;
using Crudify.Dto.Result;
using Microsoft.AspNetCore.Mvc;

namespace Crudify.Controllers
{
    [Route("api/v1/user")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet()]
        public async Task<UserResult> GetByIdAsync() => await _userService.GetByIdAsync(UserIdentity());
    }
}