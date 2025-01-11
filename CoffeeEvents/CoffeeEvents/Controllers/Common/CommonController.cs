using Application.Cities;
using CoffeeEvents.Controllers.Base.Responses;
using CoffeeEvents.Controllers.Common.Responses;
using Domain.DataQuery;
using Domain.Entities.Cities;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeEvents.Controllers.Common;

[Route("/api")]
public class CommonController : Controller
{
    private readonly ICitiesService _citiesService;

    public CommonController(ICitiesService citiesService)
    {
        _citiesService = citiesService;
    }
    
    [HttpGet("cities")]
    [ProducesResponseType(typeof(CitiesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseStatusResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCities([FromQuery] int? skip, [FromQuery] int? take)
    {
        var cities = await _citiesService.GetShortCitiesInfo(new CityQuery
        {
            Paging = new PagingParams
            {
                Skip = skip ?? 0,
                Take = take ?? 100
            },
            Sorting = new SortingParams<FullCityModel>
            {
                OrderBy = c => c.Population,
                Ascending = false
            }
        });
        var res = new CitiesResponse
        {
            Cities = cities.Select(c => new CityResponse
            {
                Name = c.Name,
                Population = c.Population
            }).ToArray()
        };
        return Ok(res);
    }
}