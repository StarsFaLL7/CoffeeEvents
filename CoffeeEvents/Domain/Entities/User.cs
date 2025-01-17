﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;

namespace Domain.Entities;

public class User : IHasId
{
    [Key]
    public Guid Id { get; set; }
    
    public required string Phone { get; set; }
    
    public required string Fio { get; set; }
    
    public required string Email { get; set; }
    
    public required string PasswordHash { get; set; }
    
    public Guid RoleId { get; set; }
    [ForeignKey("RoleId")] 
    public UserRole Role { get; set; } = null!;
    
    public required string UserStatus { get; set; }

    public string Description { get; set; } = null!;

    public string City { get; set; } = null!;
    
    public string AvatarImageFilepath { get; set; } = null!;
}