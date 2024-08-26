namespace CatalogService.Domain.Entities;
public class Color : BaseEntity
{
    public virtual ICollection<Stock>? Stocks { get; set; }
}
