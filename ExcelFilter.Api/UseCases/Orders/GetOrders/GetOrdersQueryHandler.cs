using AutoFilter;
using ExcelFilter.Api.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExcelFilter.Api.UseCases.Orders.GetOrders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, GetOrdersResponseDto[]>
{
    private readonly AppDbContext _dbContext;

    public GetOrdersQueryHandler(AppDbContext dbContext) => _dbContext = dbContext;

    public Task<GetOrdersResponseDto[]> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        return _dbContext.Orders
            .AutoFilter(request.Filter)
            .Select(x => new GetOrdersResponseDto
            {
                Id = x.Id,
                CityId = x.CityId,
                CityName = x.City.Name,
                Name = x.Name,
                Price = x.Price
            })
            .ToArrayAsync(cancellationToken);
    }
}