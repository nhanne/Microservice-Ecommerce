using InventoryService.Domain.Entities;
using InventoryService.Domain.Abstractions.Repositories;
using InventoryService.Infrastructure.Context;

namespace InventoryService.Infrastructure.Repositories;

public class InventoryRepository : RepositoryBase<Inventory>, IInventoryRepository
{
    public InventoryRepository(IMongoContext context) : base(context, "Inventories")
    {
    }
}
