using MediatR;

namespace ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptionsDynamic;

public record GetOrderFilterOptionsDynamicQuery(string[] Fields) : IRequest<GetOrderFilterOptionsDynamicResponseDto>;
