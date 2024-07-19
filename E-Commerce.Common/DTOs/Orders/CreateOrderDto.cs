using E_Commerce.Constants;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.Orders;

public class CreateOrderDto
{
    public Guid Id { get; set; } = new Guid();

    [StringLength(ClothingConstants.NoteLength)]
    public string? Note { get; set; }
}