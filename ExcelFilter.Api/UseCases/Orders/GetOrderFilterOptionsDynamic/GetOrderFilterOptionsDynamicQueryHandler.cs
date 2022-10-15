using System.Linq.Dynamic.Core;
using ExcelFilter.Api.DataAccess;
using ExcelFilter.Api.Entities;
using MediatR;

namespace ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptionsDynamic;

public class GetOrderFilterOptionsDynamicQueryHandler : IRequestHandler<GetOrderFilterOptionsDynamicQuery, GetOrderFilterOptionsDynamicResponseDto>
{
    private readonly AppDbContext _dbContext;
    private readonly FilterOptionsDynamicMapper _filterOptionsDynamicMapper;

    public GetOrderFilterOptionsDynamicQueryHandler(AppDbContext dbContext, FilterOptionsDynamicMapper filterOptionsDynamicMapper)
    {
        _dbContext = dbContext;
        _filterOptionsDynamicMapper = filterOptionsDynamicMapper;
    }

    public async Task<GetOrderFilterOptionsDynamicResponseDto> Handle(GetOrderFilterOptionsDynamicQuery request, CancellationToken cancellationToken)
    {
        var result = new Dictionary<string, FilterOption[]>();

        foreach (var field in request.Fields)
        {
            var map  = _filterOptionsDynamicMapper.GetMap(nameof(Order), field);
            result[field] = await GetFilterOptionsDynamicAsync(map.KeyName, map.LabelName, map.OrderBy, map.Converter, cancellationToken);
        }

        return new GetOrderFilterOptionsDynamicResponseDto
        {
            FilterOptions = result
        };
    }

    private async Task<FilterOption[]> GetFilterOptionsDynamicAsync(string keyField, string labelField, string orderByField, Func<object, object, FilterOption> converter, CancellationToken cancellationToken)
    {
        var options = await _dbContext.Orders
            .Select($"new ({keyField} as Key, {labelField} as Label)")
            .Where("Key != null")
            .Distinct()
            .OrderBy(orderByField)
            .ToDynamicListAsync(cancellationToken);

        return options
            .Select(x => (FilterOption)converter(x.Key, x.Label))
            .ToArray();
    }
}