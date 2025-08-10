using Catalog.Domain;

namespace Catalog.Application.Products.Contracts;

public interface IProductRepository
{
    Task<Guid> AddAsync(Product product, CancellationToken ct);
    Task<IReadOnlyList<ProductDto>> ListAsync(CancellationToken ct);
}

public sealed record ProductDto(Guid Id, string Name, decimal Price, int Stock);