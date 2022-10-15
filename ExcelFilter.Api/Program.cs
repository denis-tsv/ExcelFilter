using ExcelFilter.Api.DataAccess;
using ExcelFilter.Api.Entities;
using ExcelFilter.Api.UseCases;
using ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptions;
using ExcelFilter.Api.UseCases.Orders.GetOrderFilterOptionsDynamic;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(GetOrderFilterOptionsQuery));
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("ExcelFilter")));

var filter = new FilterOptionsMapper();

filter.RegisterMap(nameof(Order), nameof(Order.Price),
    (sp, cancellationToken) => sp.GetRequiredService<AppDbContext>()
        .Orders
        .ToFilterOptions(x => x.Price, x => x.Price.ToString("C"))
        .Distinct()
        .OrderBy(x => x.Key)
        .ToArrayAsync(cancellationToken)
);
filter.RegisterMap(nameof(Order), nameof(Order.Name),
    (sp, cancellationToken) => sp.GetRequiredService<AppDbContext>()
        .Orders
        .ToFilterOptions(x => x.Name!, x => x.Name!)
        .Where(x => x.Key != null) // can filter empty values
        .Distinct()
        .OrderBy(x => x.Key)
        .ToArrayAsync(cancellationToken)
);
filter.RegisterMap(nameof(Order), nameof(Order.CityId),
    (sp, cancellationToken) =>
    {
        var dbContext = sp.GetRequiredService<AppDbContext>();

        return dbContext.Orders
            .Join(dbContext.Cities, o => o.CityId, c => c.Id, (o, c) => c)
            .Distinct()
            .ToFilterOptions(x => x.Id, x => x.Name)
            .OrderBy(x => x.Label) // order by name, but not by id
            .ToArrayAsync(cancellationToken);
    });

builder.Services.AddSingleton(filter);


var filterDynamic = new FilterOptionsDynamicMapper();

filterDynamic.RegisterMap(nameof(Order), nameof(Order.Price),
    nameof(Order.Price),
    nameof(Order.Price),
    nameof(FilterOption.Key),
    (k, l) => new FilterOption
    {
        Key = k,
        Label = ((decimal)l).ToString("C")
    });

filterDynamic.RegisterMap(nameof(Order), nameof(Order.Name),
    nameof(Order.Name),
    nameof(Order.Name),
    nameof(FilterOption.Key),
    (k, l) => new FilterOption
    {
        Key = k,
        Label = (string)l
    });

filterDynamic.RegisterMap(nameof(Order), nameof(Order.CityId),
    nameof(Order.CityId),
    $"{nameof(Order.City)}.{nameof(City.Name)}",
    nameof(FilterOption.Label),
    (k, l) => new FilterOption
    {
        Key = k,
        Label = (string)l
    });

builder.Services.AddSingleton(filterDynamic);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();