using E_Commerce.Databases;
using E_Commerce.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories;

public abstract  class SqlRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly StoreDbContext _database;
    private readonly DbSet<TEntity> _dbSet;
    
    protected SqlRepository(StoreDbContext context)
    {
        _database = context;
        _dbSet = context.Set<TEntity>();
    }
    
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task<TEntity> AddAsync(TEntity obj)
    {
        var item = await _dbSet.AddAsync(obj);
        await _database.SaveChangesAsync();
        return item.Entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity obj)
    {
        _database.Entry(obj).State = EntityState.Modified;
        await _database.SaveChangesAsync();
        return obj;
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<bool> RemoveAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        _dbSet.Remove(entity);
        await _database.SaveChangesAsync();
        return true;
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}