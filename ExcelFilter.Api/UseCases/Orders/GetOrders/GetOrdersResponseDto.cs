namespace ExcelFilter.Api.UseCases.Orders.GetOrders;

public class GetOrdersResponseDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? CityId { get; set; }
    public string? CityName { get; set; }
    public decimal Price { get; set; }
}