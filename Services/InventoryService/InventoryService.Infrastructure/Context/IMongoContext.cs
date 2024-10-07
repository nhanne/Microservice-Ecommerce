﻿using MongoDB.Driver;

namespace InventoryService.Infrastructure.Context;

public interface IMongoContext : IDisposable
{
    void AddCommand(Func<Task> func);
    Task<int> SaveChanges();
    IMongoCollection<T> GetCollection<T>(string name);
}
