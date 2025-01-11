using CoffeeEvents.Controllers.Events.Responses;

namespace CoffeeEvents.Controllers.EventSignupWindows.Responses;

public class SignupWindowsResponse
{
    public required SignupWindowResponse[] SignupWindows { get; set; }
}