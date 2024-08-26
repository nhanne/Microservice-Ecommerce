//using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.Domain.Entities;
public class User //: IdentityUser
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    [MaxLength(255)]
    public string? Address { get; set; }
}
