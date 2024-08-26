namespace CatalogService.Domain.Entities;
public class Stock
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid ColorId { get; set; }
    public Guid SizeId { get; set; }
    
    public int Quantity { get; set; }
    
    public virtual Product? Product { get; set; }
    public virtual Color? Color { get; set; }
    public virtual Size? Size { get; set; }
    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }

}