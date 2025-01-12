using System.ComponentModel.DataAnnotations;
using Domain.Interfaces;

namespace Domain.Entities.Cities;

public class FullCityModel
{
    public required CoordsModel Coords { get; set; }
    
    public required string District { get; set; }
    
    public required string Name { get; set; }
    
    public int Population { get; set; }
    
    public required string Subject { get; set; }
}