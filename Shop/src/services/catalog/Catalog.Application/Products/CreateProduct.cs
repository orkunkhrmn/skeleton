using Catalog.Application.Products.Contracts;
using Catalog.Domain;
using MediatR;

namespace Catalog.Application.Products;

public sealed record CreateProductCommand(string Name, decimal Price, int Stock): IRequest<Guid>;

public sealed class CreateProductHandler(IProductRepository repo) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand cmd, CancellationToken ct)
    {
        var entity = new Product(cmd.Name, cmd.Price, cmd.Stock);

        return await repo.AddAsync(entity, ct);
    }
}