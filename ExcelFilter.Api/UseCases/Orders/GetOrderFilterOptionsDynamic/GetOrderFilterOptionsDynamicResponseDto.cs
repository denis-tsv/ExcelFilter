namespace ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptionsDynamic;

public class GetOrderFilterOptionsDynamicResponseDto
{
    public Dictionary<string, FilterOption[]> FilterOptions { get; set; } = null!;
}