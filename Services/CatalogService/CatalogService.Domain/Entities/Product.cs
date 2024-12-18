﻿using System.ComponentModel.DataAnnotations;
using CatalogService.Common.Constants;

namespace CatalogService.Domain.Entities;
public class Product : BaseEntity
{
    public Guid CategoryId { get; set; }
    
    [StringLength(ClothingConstants.ImageLength)]
    public string? Image { get; set; }
    
    public float CostPrice { get; set; }
    
    public float UnitPrice { get; set; }

    public virtual Category? Category { get; set; }
    public virtual ICollection<Stock>? Stocks { get; set; }

}
