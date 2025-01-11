using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class RefreshToken
{
    [Key]
    public Guid UserId { get; set; }
    
    public required string Token { get; set; }
    
    public DateTime ExpiryDate { get; set; }
}