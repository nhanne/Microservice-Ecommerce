namespace CatalogService.Domain.Entities;
public class Size : BaseEntity
{
    public virtual ICollection<Stock>? Stocks { get; set; }
}
