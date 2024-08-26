namespace CatalogService.Domain.Entities;
public class OrderDetail
{
    public Guid OrderId { get; set; }
    public Guid StockId { get; set; }
    public int Quantity { get; set; }
    public float UnitPrice { get; set; }
    public virtual Order? Order { get; set; }
    public virtual Stock? Stock { get; set; } 
}