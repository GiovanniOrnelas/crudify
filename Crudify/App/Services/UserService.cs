using Crudify.Domain.Interfaces.Repository;
using Crudify.Domain.Interfaces.Services;
using Crudify.Dto.Result;

namespace Crudify.App.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResult> GetByIdAsync(long id)
        {
            var response = await _userRepository.GetByIdAsync(id);

            return (UserResult)response;
        }
    }
}