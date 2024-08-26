namespace CatalogService.Common.DTOs.Colors;
public class ColorDto
{
    public Guid Id { get; set; } 
    
    public string Code { get; set; } = string.Empty;
  
    public string Name { get; set; } = string.Empty;
    
    public string? Note { get; set; }
}