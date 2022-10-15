using MediatR;

namespace ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptions;

public class GetOrderFilterOptionsRequestDto : IRequest<GetOrderFilterOptionsResponseDto>
{
    public string[] Fields { get; set; } = null!;
}