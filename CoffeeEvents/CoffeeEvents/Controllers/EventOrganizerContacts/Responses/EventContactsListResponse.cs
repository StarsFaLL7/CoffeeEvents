using CoffeeEvents.Controllers.Events.Responses;

namespace CoffeeEvents.Controllers.EventOrganizerContacts.Responses;

public class EventContactsListResponse
{
    public required ContactsResponse[] Contacts { get; set; }
}