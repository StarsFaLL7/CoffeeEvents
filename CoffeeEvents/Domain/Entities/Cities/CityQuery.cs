using System.Linq.Expressions;
using Domain.DataQuery;

namespace Domain.Entities.Cities;

public class CityQuery
{
    public Expression<Func<FullCityModel, bool>>? Expression { get; set; }
    
    public PagingParams? Paging { get; set; }
    
    public SortingParams<FullCityModel>? Sorting { get; set; }
    
    public List<Expression<Func<FullCityModel, bool>>>? Filters { get; set; }
}