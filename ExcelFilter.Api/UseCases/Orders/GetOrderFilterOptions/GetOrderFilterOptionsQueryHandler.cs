using ExcelFilter.Api.Entities;
using MediatR;

namespace ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptions;

public class GetOrderFilterOptionsQueryHandler : IRequestHandler<GetOrderFilterOptionsQuery, GetOrderFilterOptionsResponseDto>
{
    private readonly FilterOptionsMapper _filterOptionsMapper;
    private readonly IServiceProvider _serviceProvider;

    public GetOrderFilterOptionsQueryHandler(FilterOptionsMapper filterOptionsMapper, IServiceProvider serviceProvider)
    {
        _filterOptionsMapper = filterOptionsMapper;
        _serviceProvider = serviceProvider;
    }

    public async Task<GetOrderFilterOptionsResponseDto> Handle(GetOrderFilterOptionsQuery request, CancellationToken cancellationToken)
    {
        var result = new Dictionary<string, FilterOption[]>();

        foreach (var field in request.Fields)
        {
            result[field] = await _filterOptionsMapper.GetMap(nameof(Order), field)(_serviceProvider, cancellationToken);
        }

        return new GetOrderFilterOptionsResponseDto
        {
            FilterOptions = result
        };
    }
}