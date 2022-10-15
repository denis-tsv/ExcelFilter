using ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptions;
using ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptionsDynamic;
using ExcelFilter.Api.UseCases.Orders.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExcelFilter.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ISender _sender;

    public OrdersController(ISender sender) => _sender = sender;

    /// <summary>
    /// Available fields: Price, Name, CityId
    /// </summary>
    [HttpGet("filter-options")]
    public async Task<GetOrderFilterOptionsResponseDto> GetFilterOptions([FromQuery] GetOrderFilterOptionsRequestDto dto, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetOrderFilterOptionsQuery(dto.Fields), cancellationToken);
        
        return result;
    }

    /// <summary>
    /// Available fields: Price, Name, CityId
    /// </summary>
    [HttpGet("filter-options-dynamic")]
    public async Task<GetOrderFilterOptionsDynamicResponseDto> GetFilterOptionsDynamic([FromQuery] GetOrderFilterOptionsRequestDto dto, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetOrderFilterOptionsDynamicQuery(dto.Fields), cancellationToken);

        return result;
    }

    [HttpPost]
    public async Task<GetOrdersResponseDto[]> GetOrders([FromBody] GetOrdersRequestDto dto, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetOrdersQuery(dto), cancellationToken);

        return result;
    }
}