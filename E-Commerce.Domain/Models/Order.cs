﻿using System.ComponentModel.DataAnnotations;
using E_Commerce.Constants;
using E_Commerce.Enums;

namespace E_Commerce.Models;

public class Order
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = ClothingConstants.Required)]
    [StringLength(ClothingConstants.AddressLength)]
    public string Address { get; set; } = string.Empty;
    
    public OrderStatus Status { get; set; }
    
    public OrderPayment Payment { get; set; }
    
    public float TotalPrice { get; set; }
    
    [StringLength(ClothingConstants.NoteLength)]
    public string? Note { get; set; }
    
    public DateTime TimeCreated { get; set; }
    
    public DateTime TimeShipped { get; set; }

    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
}



