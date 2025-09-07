using Microsoft.EntityFrameworkCore;

namespace Crudify.Domain.Interfaces.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        DbSet<TEntity> GetContext();
        IQueryable<TEntity> Context { get; }
        Task<IEnumerable<TEntity>> InsertRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}