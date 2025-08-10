using Catalog.Application.Products.Contracts;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<CatalogDbContext>(opt => opt.UseSqlServer(connectionString));
        services.AddScoped<IProductRepository, EfProductRepository>();
        return services;
    }
}