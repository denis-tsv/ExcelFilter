namespace ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptionsSimple;

public class GetOrderFilterOptionsSimpleResponseDto
{
    public Dictionary<string, FilterOption[]> FilterOptions { get; set; } = null!;
}