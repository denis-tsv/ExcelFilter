using MediatR;

namespace ExcelFilter.Api.UseCases.Orders.GetOrders;

public record GetOrdersQuery(GetOrdersRequestDto Filter) : IRequest<GetOrdersResponseDto[]>;
