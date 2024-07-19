using AutoMapper;
using E_Commerce.DTOs.Colors;
using E_Commerce.Models;

namespace E_Commerce;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Color, ColorDto>();
        CreateMap<CreateColorDto, Color>();
        CreateMap<UpdateColorDto, Color>();
    }
}