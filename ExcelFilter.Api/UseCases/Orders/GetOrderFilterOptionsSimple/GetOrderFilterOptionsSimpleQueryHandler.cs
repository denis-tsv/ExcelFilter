using ExcelFilter.Api.Entities;
using MediatR;

namespace ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptionsSimple;

public class GetOrderFilterOptionsSimpleQueryHandler : IRequestHandler<GetOrderFilterOptionsSimpleQuery, GetOrderFilterOptionsSimpleResponseDto>
{
    private readonly FilterOptionsSimpleMapper _filterOptionsSimpleMapper;
    private readonly IServiceProvider _serviceProvider;

    public GetOrderFilterOptionsSimpleQueryHandler(FilterOptionsSimpleMapper filterOptionsSimpleMapper, IServiceProvider serviceProvider)
    {
        _filterOptionsSimpleMapper = filterOptionsSimpleMapper;
        _serviceProvider = serviceProvider;
    }

    public async Task<GetOrderFilterOptionsSimpleResponseDto> Handle(GetOrderFilterOptionsSimpleQuery request, CancellationToken cancellationToken)
    {
        var result = new Dictionary<string, FilterOption[]>();

        foreach (var field in request.Fields)
        {
            result[field] = await _filterOptionsSimpleMapper.GetMap(nameof(Order), field)(_serviceProvider, cancellationToken);
        }

        return new GetOrderFilterOptionsSimpleResponseDto
        {
            FilterOptions = result
        };
    }
}