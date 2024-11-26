using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public required string Phone { get; set; }
    
    public required string Fio { get; set; }
    
    public required string Email { get; set; }
    
    public required string PasswordHash { get; set; }
    
    public Guid RoleId { get; set; }
    [ForeignKey("RoleId")] 
    public IdentityRole<Guid> Role { get; set; } = null!;
    
    public required string UserStatus { get; set; }

    public string Description { get; set; } = null!;

    public string City { get; set; } = null!;
    
    public string AvatarImageFilepath { get; set; } = null!;
    
}