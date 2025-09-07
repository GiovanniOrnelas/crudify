using Crudify.Domain.Entities;
using Crudify.Domain.Interfaces.Repository;
using Crudify.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Crudify.Infrastructure.Repository
{
    public class TenantRepository : GenericRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(DataContext context) : base(context) {}

        public override DbSet<Tenant> GetContext() => Ctx.Tenants;
    }
}