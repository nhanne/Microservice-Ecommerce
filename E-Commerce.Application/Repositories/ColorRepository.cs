using E_Commerce.Databases;
using E_Commerce.Models;

namespace E_Commerce.Repositories;

public class ColorRepository : SqlRepository<Color>
{
    public ColorRepository(StoreDbContext context) : base(context)
    {
    }
}