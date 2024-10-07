namespace InventoryService.Infrastructure.UoW;

public interface IUnitOfWork : IDisposable
{
    Task<bool> Commit();
}
