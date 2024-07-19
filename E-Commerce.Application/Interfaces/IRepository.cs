﻿namespace E_Commerce.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> AddAsync(TEntity obj);
    Task<TEntity> UpdateAsync(TEntity obj);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<bool> RemoveAsync(Guid id);
}