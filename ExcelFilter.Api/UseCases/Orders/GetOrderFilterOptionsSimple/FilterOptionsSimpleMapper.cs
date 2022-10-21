namespace ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptionsSimple;

public class FilterOptionsSimpleMapper
{
    private readonly Dictionary<(string, string), Func<IServiceProvider, CancellationToken, Task<FilterOption[]>>> _maps = new();

    public void RegisterMap(string entityName, string fieldName, Func<IServiceProvider, CancellationToken, Task<FilterOption[]>> getOptions)
        => _maps.Add((entityName, fieldName), getOptions);

    public Func<IServiceProvider, CancellationToken, Task<FilterOption[]>> GetMap(string entityName, string fieldName)
        => _maps[(entityName, fieldName)];
}