using MediatR;

namespace ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptions;

public record GetOrderFilterOptionsQuery(string[] Fields) : IRequest<GetOrderFilterOptionsResponseDto>;