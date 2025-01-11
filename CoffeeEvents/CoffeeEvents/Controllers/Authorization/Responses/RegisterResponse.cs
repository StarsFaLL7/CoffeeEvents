using CoffeeEvents.Controllers.Base.Responses;

namespace CoffeeEvents.Controllers.Authorization.Responses;

public class RegisterResponse : BaseStatusResponse
{
    public Guid? UserId { get; set; }
    
    public required string AccessToken { get; set; }
}