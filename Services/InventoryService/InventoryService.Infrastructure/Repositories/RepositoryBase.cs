using MongoDB.Driver;
using InventoryService.Infrastructure.Context;
using InventoryService.Infrastructure.Extentions;
using InventoryService.Domain.Abstractions.Repositories;

namespace InventoryService.Infrastructure.Repositories;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected readonly IMongoContext _context;

    protected readonly IMongoCollection<TEntity> _collection;

    protected RepositoryBase(IMongoContext context, string collectionName)
    {
        _context = context;
        _collection = _context.GetCollection<TEntity>(collectionName);
    }

    public virtual void Add(TEntity entity)
    {
        _context.AddCommand(() => _collection.InsertOneAsync(entity));
    }

    public virtual void Update(TEntity entity)
    {
        _context.AddCommand(() => _collection.ReplaceOneAsync(
            Builders<TEntity>.Filter.Eq("_id", EntityExtension.GetId(entity)), entity));
    }

    public virtual void Remove(Guid id)
    {
        _context.AddCommand(() => _collection.DeleteOneAsync(
           Builders<TEntity>.Filter.Eq("_id", id)));
    }

    public virtual async Task<TEntity> GetById(Guid id)
    {
        var data = await _collection.FindAsync(
            Builders<TEntity>.Filter.Eq("_id", id));
        return data.SingleOrDefault();
    }

    public virtual IQueryable<TEntity> GetQueryable()
    {
        return _collection.AsQueryable();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
