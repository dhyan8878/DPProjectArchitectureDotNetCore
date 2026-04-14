using DP.Domain.Common;

namespace DP.Domain.Entities;

public class Order : BaseEntity
{
    public string CustomerName { get; private set; } = default!;
    public DateTime OrderDate { get; private set; }
    public decimal TotalAmount { get; private set; }

    //Encapsulation (OrderItems):Prevents:Direct modification, Enforces business rules via methods
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    private Order() { } // EF Core

    public Order(string customerName)
    {
        CustomerName = customerName;
        OrderDate = DateTime.UtcNow;
    }

    public void AddOrderItem(Guid productId, int quantity, decimal unitPrice)
    {
        var orderItem = new OrderItem(Id, productId, quantity, unitPrice);
        _orderItems.Add(orderItem);

        RecalculateTotal();
    }

    //Domain Logic Inside Entities: Business logic belongs to Domain, not services
    private void RecalculateTotal()
    {
        TotalAmount = _orderItems.Sum(x => x.Quantity * x.UnitPrice);
    }
}