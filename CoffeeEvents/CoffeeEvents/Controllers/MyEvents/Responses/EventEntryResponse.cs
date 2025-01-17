﻿using CoffeeEvents.Controllers.UserInfo.Responses;

namespace CoffeeEvents.Controllers.MyEvents.Responses;

public class EventEntryResponse
{
    public Guid EntryId { get; set; }
    
    public BriefUserInfoResponse? UserInfo { get; set; }
    
    public string? Phone { get; set; }
    
    public string? Fio { get; set; }
    
    public string? Email { get; set; }
    
    public required DateTime DateTime { get; set; }
    
    public required Dictionary<string, string> DynamicFieldsData { get; set; }
}