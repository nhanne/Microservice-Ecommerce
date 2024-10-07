using AutoMapper;
using InventoryService.Application.DTOs;
using InventoryService.Domain.Entities;

namespace InventoryService.Application.Mappers;

public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        CreateMap<Inventory, InventoryDto>().ForMember(dest => dest.Id, opt =>
            opt.MapFrom(src => Guid.Parse(src.Id.ToString()))
        );

        CreateMap<InventoryDto, CreateInventoryDto>();
    }
}
