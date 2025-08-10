using Catalog.Application.Products;
using Catalog.Infrastructure.Configuration;
using Catalog.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductsQuery).Assembly));

var cs = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddCatalogInfrastructure(cs);

// Health
builder.Services.AddHealthChecks()
    .AddDbContextCheck<CatalogDbContext>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapHealthChecks("/health");

// Auto migration
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    db.Database.Migrate();
}

app.MapPost("/products", async (CreateProductCommand cmd, IMediator mediator) =>
{
    var id = await mediator.Send(cmd);
    return Results.Created($"/products/{id}", new { id });
});

app.MapGet("/products", async(IMediator mediator) =>
{
    var list = await mediator.Send(new GetProductsQuery());
    return Results.Ok(list);
});

app.Run();
