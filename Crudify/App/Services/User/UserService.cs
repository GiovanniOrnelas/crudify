using Crudify.Domain.Interfaces.Services;
using Crudify.Dto;
using Crudify.Infrastructure.Repository;

namespace Crudify.App.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
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