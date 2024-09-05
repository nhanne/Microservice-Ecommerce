namespace CatalogService.Application.Interfaces;
public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> AddAsync(TEntity obj);
    Task<TEntity> UpdateAsync(TEntity obj);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<bool> RemoveAsync(Guid id);
}