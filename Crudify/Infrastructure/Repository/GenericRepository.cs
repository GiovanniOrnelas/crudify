using Crudify.Domain.Interfaces;
using Crudify.Domain.Interfaces.Repository;
using Crudify.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Crudify.Infrastructure.Repository
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected DataContext Ctx { get; }

        public GenericRepository(DataContext context)
        {
            Ctx = context;
        }

        public virtual IQueryable<TEntity> Context => GetContext();

        public virtual async Task<TEntity> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await GetContext().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public virtual async Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity.Id.Equals(0))
                return await InsertAsync(entity, cancellationToken);

            var original = await GetByIdAsync(entity.Id, cancellationToken);

            if (original == null)
                return await InsertAsync(entity, cancellationToken);

            return await UpdateAsync(original, entity);
        }

        public abstract DbSet<TEntity> GetContext();

        public virtual async Task<IEnumerable<TEntity>> InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            await GetContext().AddRangeAsync(entities);
            await Ctx.SaveChangesAsync();
            return entities;
        }

        private async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = await GetContext().AddAsync(entity, cancellationToken);
            await Ctx.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        private async Task<TEntity> UpdateAsync(TEntity original, TEntity modified, CancellationToken cancellationToken = default)
        {
            Ctx.Entry(original).CurrentValues.SetValues(modified);
            await Ctx.SaveChangesAsync(cancellationToken);
            return original;
        }
    }
}