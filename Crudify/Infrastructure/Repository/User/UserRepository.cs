using Crudify.Domain.Entities;
using Crudify.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Crudify.Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DataContext context) : base(context) { }

        public override DbSet<User> GetContext() => Ctx.Users;
    }
}