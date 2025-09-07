using Crudify.App.Services;
using Crudify.Commons.Enums;
using Crudify.Domain.Entities;
using Crudify.Domain.Interfaces.Repository;
using Moq;

namespace Crudify.Test.App.Services
{
    public class UserServiceTest
    {
        [Fact]
        public async Task GetByIdAsync_ShouldReturnUserWithSameId()
        {
            var userId = 1;
            var user = new User("Crudify Admin", "admin123", UserType.Management, "admin@crudify.com", "111.111.111-11", DateTime.UtcNow.AddYears(-20), Gender.Male, 1) { Id = userId };

            var repoMock = new Mock<IUserRepository>();
            repoMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(user);

            var service = new UserService(repoMock.Object);

            var result = await service.GetByIdAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(user.Name, result.Name);
        }
    }
}