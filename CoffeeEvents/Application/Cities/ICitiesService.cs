using Domain.Entities.Cities;

namespace Application.Cities;

public interface ICitiesService
{
    public Task<FullCityModel[]> GetFullCitiesInfo(CityQuery queryParams);
    
    public Task<ShortCityModel[]> GetShortCitiesInfo(CityQuery queryParams);
}