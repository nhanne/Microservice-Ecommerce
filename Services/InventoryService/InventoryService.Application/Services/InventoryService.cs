using AutoMapper;
using InventoryService.Application.Abstractions;
using InventoryService.Application.DTOs;
using InventoryService.Domain.Abstractions.Repositories;
using InventoryService.Domain.Entities;
using InventoryService.Infrastructure.UoW;

namespace InventoryService.Application.Services;

public class InventoryService : IInventoryService
{
    private readonly IUnitOfWork _uow;
    private readonly IInventoryRepository _repository;
    private readonly IMapper _mapper;

    public InventoryService(IUnitOfWork uow,
                            IInventoryRepository repository, 
                            IMapper mapper)
    {
        _uow = uow;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<InventoryDto> CreateAsync(CreateInventoryDto inputModel)
    {
        var model = new Inventory
        {
            ProductName = inputModel.ProductName,
            Brand = inputModel.Brand,
            Category = inputModel.Category,
            Price = inputModel.Price,
            Color = inputModel.Color,
            Size = inputModel.Size,
            Description = inputModel.Description, 
            Quantity = inputModel.Quantity,
        };
        _repository.Add(model);

        await _uow.Commit();

        return _mapper.Map<Inventory, InventoryDto>(model);
    }

    public async Task<IEnumerable<InventoryDto>> GetAllAsync()
    {
        var listInventories = _repository.GetQueryable().AsEnumerable();
        var result = _mapper.Map<IEnumerable<Inventory>, IEnumerable<InventoryDto>>(listInventories);
        return await Task.FromResult(result);
    }

    public Task RemoveAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync()
    {
        throw new NotImplementedException();
    }

    public Task GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
