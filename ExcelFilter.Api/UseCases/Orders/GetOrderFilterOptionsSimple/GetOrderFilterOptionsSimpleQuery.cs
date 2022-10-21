using MediatR;

namespace ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptionsSimple;

public record GetOrderFilterOptionsSimpleQuery(string[] Fields) : IRequest<GetOrderFilterOptionsSimpleResponseDto>;
