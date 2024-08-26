using AutoMapper;
using CatalogService.Common.Colors;
using CatalogService.Common.DTOs.Colors;
using CatalogService.Domain.Entities;

namespace CatalogService.Application;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Color, ColorDto>();
        CreateMap<CreateColorDto, Color>();
        CreateMap<UpdateColorDto, Color>();
    }
}