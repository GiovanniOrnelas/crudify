using Crudify.App.Services;
using Crudify.Infrastructure.EF;
using Crudify.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Crudify.Domain.Entities;
using Crudify.Commons.Enums;

namespace Crudify.Test.App.Services
{
    public class UserServiceTest
    {
        private DataContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "UserServiceTestDb")
                .Options;

            return new DataContext(options);
        }

        private UserService GetService(DataContext context)
        {
            var repository = new UserRepository(context);
            return new UserService(repository);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUserWithSameId()
        {
            var context = GetInMemoryDbContext();

            context.Add(new User("Crudify Admin", "admin123", UserType.Management, "admin@crudify.com", "111.111.111-11", DateTime.UtcNow.AddYears(-20), Gender.Male));
            await context.SaveChangesAsync();

            var user = context.Users.First();

            var service = GetService(context);

            var result = await service.GetByIdAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Name.ToLower(), result.Name.ToLower());
        }
    }
}
