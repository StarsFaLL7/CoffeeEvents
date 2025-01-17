﻿namespace CoffeeEvents.Controllers.Events.Responses;

public class ContactsResponse
{
    public required Guid Id { get; set; }
    
    public required string Email { get; set; }
    
    public string Fio { get; set; } = null!;

    public string Phone { get; set; } = null!;
    
    public string Telegram { get; set; } = null!;
}