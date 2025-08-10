using Catalog.Application.Products.Contracts;
using MediatR;

namespace Catalog.Application.Products;

public sealed record GetProductsQuery():IRequest<IReadOnlyList<ProductDto>>;

public sealed class GetProductsHandler(IProductRepository repo) : IRequestHandler<GetProductsQuery, IReadOnlyList<ProductDto>>
{
    public Task<IReadOnlyList<ProductDto>> Handle(GetProductsQuery q, CancellationToken ct)
    {
        repo.ListAsync(ct);
    }
}