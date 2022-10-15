using ExcelFilter.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExcelFilter.Api.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }

    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>()
            .HasData(
                new City { Id = 1, Name = "City 2" },
                new City { Id = 2, Name = "City 1" }
            );

        modelBuilder.Entity<Order>()
            .HasData(
                new Order { Id = 1, CreatedAt = DateTime.Now, CityId = 1, Price = (decimal)100.1, Name = null },
                new Order { Id = 2, CreatedAt = DateTime.Now.AddDays(1).AddMonths(1).AddYears(1), CityId = 2, Price = (decimal)20.25, Name = "Order 2" },
                new Order { Id = 3, CreatedAt = DateTime.Now.AddDays(-1).AddMonths(-1).AddYears(-1), CityId = null, Price = 50, Name = "Order 3" },
                new Order { Id = 4, CreatedAt = DateTime.Now.AddDays(-1).AddMonths(-1), CityId = null, Price = 10, Name = null },
                new Order { Id = 5, CreatedAt = DateTime.Now.AddDays(-1).AddMonths(-1), CityId = 1, Price = 15, Name = "Order 1" }
            );
    }
}