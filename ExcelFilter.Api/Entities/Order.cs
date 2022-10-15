namespace ExcelFilter.Api.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Price { get; set; }
    public string? Name { get; set; }
    public int? CityId { get; set; }
    public City? City { get; set; }
}