using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;

namespace Domain.Entities;

public class EventSignupEntry : IHasId
{
    [Key]
    public Guid Id { get; set; }
    
    public required Guid EventId { get; set; }
    [ForeignKey("EventId")]
    public UserEvent Event { get; set; } = null!;

    public required Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; } = null!;
    
    public string? Phone { get; set; } = null!;
    
    public string? Email { get; set; } = null!;
}