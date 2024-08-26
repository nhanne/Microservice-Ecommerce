using CatalogService.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.Domain.Entities;
public abstract class BaseEntity
{
    public Guid Id { get; set; } 
    
    [StringLength(ClothingConstants.CodeLength)]
    [Required(ErrorMessage = ClothingConstants.Required)]
    public string Code { get; set; } = string.Empty;
    
    [StringLength(ClothingConstants.NameLength)]
    [Required(ErrorMessage = ClothingConstants.Required)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(ClothingConstants.NoteLength)]
    public string? Note { get; set; }

    public bool IsDelete { get; set; }

    public DateTime TimeCreated { get; set; } = DateTime.Now;

}