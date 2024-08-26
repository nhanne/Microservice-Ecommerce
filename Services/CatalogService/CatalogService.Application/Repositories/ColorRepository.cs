using CatalogService.Infrastructure;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Repositories;
public class ColorRepository : SqlRepository<Color>
{
    public ColorRepository(StoreDbContext context) : base(context)
    {
    }
}