using System.ComponentModel.DataAnnotations;
using E_Commerce.Constants;

namespace E_Commerce.DTOs.Colors;

public class UpdateColorDto
{
    [StringLength(ClothingConstants.CodeLength)]
    [Required(ErrorMessage = ClothingConstants.Required)]
    public string Code { get; set; } = string.Empty;
    
    [StringLength(ClothingConstants.NameLength)]
    [Required(ErrorMessage = ClothingConstants.Required)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(ClothingConstants.NoteLength)]
    public string? Note { get; set; }
}