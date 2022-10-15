namespace ExcelFilter.Api.UseCases.Orders.GetOrders;

public class GetOrdersRequestDto
{
    public string[]? Name { get; set; }
    public decimal[]? Price { get; set; }
    public int[]? CityId { get; set; }
}