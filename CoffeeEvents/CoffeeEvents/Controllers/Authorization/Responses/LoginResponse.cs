using CoffeeEvents.Controllers.Base.Responses;

namespace CoffeeEvents.Controllers.Authorization.Responses;

public class LoginResponse : BaseStatusResponse
{
    public required Guid? UserId { get; set; }
    
    public required string AccessToken { get; set; }
}