using CoffeeEvents.Controllers.Base.Responses;

namespace CoffeeEvents.Controllers.UserInfo.Responses;

public class UpdateAvatarResponse : BaseStatusResponse
{
    public required string Image { get; set; }
}