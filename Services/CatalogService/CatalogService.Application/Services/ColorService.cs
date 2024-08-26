using AutoMapper;
using CatalogService.Common.DTOs.Colors;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Services;
public class ColorService : IColorService
{
    private readonly IRepository<Color> _repository;
    private readonly IMapper _mapper;
    
    public ColorService(IRepository<Color> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ColorDto>> GetAllAsync()
    {
        var listEntities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<Color>, IEnumerable<ColorDto>>(listEntities);
    }

    public async Task<ColorDto> GetByIdAsync(Guid id)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public async Task<ColorDto> CreateAsync()
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public async Task<ColorDto> UpdateAsync()
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Guid id) => await _repository.RemoveAsync(id);
}