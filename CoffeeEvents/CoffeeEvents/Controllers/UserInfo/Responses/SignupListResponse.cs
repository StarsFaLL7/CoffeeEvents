using CoffeeEvents.Controllers.Events.Responses;

namespace CoffeeEvents.Controllers.UserInfo.Responses;

public class SignupListResponse
{
    public required SignupResponse[] Entries { get; set; }
}