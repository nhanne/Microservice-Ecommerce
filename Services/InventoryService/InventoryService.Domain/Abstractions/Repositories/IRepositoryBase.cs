namespace InventoryService.Domain.Abstractions.Repositories;

public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(Guid id);
    Task<TEntity> GetById(Guid id);
    IQueryable<TEntity> GetQueryable();
}