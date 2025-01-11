using System.ComponentModel.DataAnnotations;
using Domain.Interfaces;

namespace Domain.Entities.Cities;

public class FullCityModel : IHasId
{
    [Key]
    public required Guid Id { get; set; }
    
    public required CoordsModel Coords { get; set; }
    
    public required string District { get; set; }
    
    public required string Name { get; set; }
    
    public int Population { get; set; }
    
    public required string Subject { get; set; }
}