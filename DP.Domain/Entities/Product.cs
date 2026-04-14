using DP.Domain.Common;

namespace DP.Domain.Entities;

//No EF Core attributes Here, This is PURE Domain, No Persistence Concerns, No Validation Concerns, No UI Concerns, Just Business Logic
//will use IEntityTypeConfiguration (Infrastructure layer) to configure EF Core mappings, This keeps our Domain Clean and Focused on Business Logic
public class Product : BaseEntity
{
    //Private Setters: Prevents uncontrolled updates, Forces usage of business methods
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }

    private Product() { } // For EF Core

    //Constructors Enforce Valid State: Entity can NEVER be invalid, notice private Product() above
    public Product(string name, string description, decimal price, int stockQuantity)
    {
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public void UpdateDetails(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
        SetUpdated();
    }

    public void UpdateStock(int quantity)
    {
        StockQuantity = quantity;
        SetUpdated();
    }
}