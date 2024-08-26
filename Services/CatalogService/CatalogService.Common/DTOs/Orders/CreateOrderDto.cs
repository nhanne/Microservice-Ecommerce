using CatalogService.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.Common.DTOs.Orders;
public class CreateOrderDto
{
    public Guid Id { get; set; } = new Guid();

    [StringLength(ClothingConstants.NoteLength)]
    public string? Note { get; set; }
}