using CoffeeEvents.Controllers.Events.Responses;

namespace CoffeeEvents.Controllers.DynamicFields.Responses;

public class DynamicFieldsResponse
{
    public required FormFieldResponse[] Fields { get; set; }
}