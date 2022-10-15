namespace ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptionsDynamic;

public class FilterOptionsDynamicMapper
{
    private readonly Dictionary<(string, string), (string KeyName, string LabelName, string OrderBy, Func<object, object, FilterOption> Converter)> _maps = new();

    public void RegisterMap(string entityName, string fieldName, string keyName, string labelName, string orderBy, Func<object, object, FilterOption> converter)
        => _maps.Add((entityName, fieldName), (keyName, labelName, orderBy, converter));

    public (string KeyName, string LabelName, string OrderBy, Func<object, object, FilterOption> Converter) GetMap(string entityName, string fieldName)
        => _maps[(entityName, fieldName)];
}