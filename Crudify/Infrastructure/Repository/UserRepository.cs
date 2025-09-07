using Crudify.Domain.Entities;
using Crudify.Domain.Interfaces.Repository;
using Crudify.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Crudify.Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }

        public override DbSet<User> GetContext() => Ctx.Users;
    }
}