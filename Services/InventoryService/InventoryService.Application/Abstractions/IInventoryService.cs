using InventoryService.Application.DTOs;

namespace InventoryService.Application.Abstractions;

public interface IInventoryService
{
    Task<IEnumerable<InventoryDto>> GetAllAsync();
    Task<InventoryDto> CreateAsync(CreateInventoryDto inputModel);
    Task UpdateAsync();
    Task RemoveAsync();
    Task GetAsync(Guid id);
}
