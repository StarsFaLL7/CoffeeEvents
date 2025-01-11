using System.ComponentModel.DataAnnotations;

namespace CoffeeEvents.Controllers.UserInfo.Requests;

public class UpdatePhoneRequest
{
    [Phone]
    public required string Phone { get; set; }
}