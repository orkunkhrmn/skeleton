using Catalog.Application.Products.Contracts;
using Catalog.Domain;
using Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

public sealed class EfProductRepository(CatalogDbContext db) : IProductRepository
{
    public async Task<Guid> AddAsync(Product product, CancellationToken ct)
    {
        db.Products.Add(product);
        await db.SaveChangesAsync();
        return product.Id;
    }

    public async Task<IReadOnlyList<ProductDto>> ListAsync(CancellationToken ct)
    {
        return await db.Products.AsNoTracking()
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock))
           .ToListAsync();
    }
}