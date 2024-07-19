using E_Commerce.DTOs.Colors;

namespace E_Commerce.Interfaces;

public interface IColorService
{
    Task<IEnumerable<ColorDto>> GetAllAsync();
    Task<ColorDto> GetByIdAsync(Guid id);
    Task<ColorDto> CreateAsync();
    Task<ColorDto> UpdateAsync();
    Task<bool> DeleteAsync(Guid id);
}