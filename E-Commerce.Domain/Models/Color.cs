namespace E_Commerce.Models;
public class Color : BaseEntity
{
    public virtual ICollection<Stock>? Stocks { get; set; }
}
