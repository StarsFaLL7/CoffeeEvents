using System.ComponentModel.DataAnnotations;

namespace CoffeeEvents.Controllers.UserInfo.Requests;

public class UpdateEmailRequest
{
    [EmailAddress]
    public required string Email { get; set; }
}