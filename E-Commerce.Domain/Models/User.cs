using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace E_Commerce.Models;

public class User : IdentityUser
{
    [PersonalData]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [PersonalData]
    public DateTime DateOfBirth { get; set; }
    [PersonalData]
    [MaxLength(255)]
    public string? Address { get; set; }
}
