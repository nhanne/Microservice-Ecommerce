namespace E_Commerce.Models;
public  class Size : BaseEntity
{
    public virtual ICollection<Stock>? Stocks { get; set; }
}
