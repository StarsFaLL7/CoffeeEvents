using CoffeeEvents.Controllers.Base.Responses;

namespace CoffeeEvents.Controllers.Events.Responses;

public class UpcomingEventsResponse
{
    public required BasicEventInfoResponse[] Events { get; set; }
}