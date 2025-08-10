namespace Catalog.Domain;

public sealed class Product
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    private Product() { }
    public Product(string name, decimal price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }

    public void DecreaseStock(int qty)
    {
        if (qty <= 0 || qty > Stock)
        {
            throw new InvalidOperationException("Invalid qty");
        }

        Stock -= qty;
    }
}
