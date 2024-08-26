using CatalogService.Common.DTOs.Colors;

namespace CatalogService.Application.Interfaces;
public interface IColorService
{
    Task<IEnumerable<ColorDto>> GetAllAsync();
    Task<ColorDto> GetByIdAsync(Guid id);
    Task<ColorDto> CreateAsync();
    Task<ColorDto> UpdateAsync();
    Task<bool> DeleteAsync(Guid id);
}