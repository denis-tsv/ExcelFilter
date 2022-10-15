namespace ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptions;

public class GetOrderFilterOptionsResponseDto
{
    public Dictionary<string, FilterOption[]> FilterOptions { get; set; } = null!;
}