﻿using CoffeeEvents.Controllers.Base.Responses;

namespace CoffeeEvents.Controllers.UserInfo.Responses;

public class UserInfoResponse
{
    public required Guid Id { get; set; }
    
    public required string Fio { get; set; }

    public string City { get; set; } = null!;
    
    public required string Description { get; set; }
    
    public required string Status { get; set; }
    
    public required string AvatarImage { get; set; }
    
    public required BasicEventInfoResponse[] CreatedEvents { get; set; }
}