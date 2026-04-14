using DP.Domain.Common;

namespace DP.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    // Navigation Properties (Required for EF Core)
    public Order Order { get; private set; } = default!;
    public Product Product { get; private set; } = default!;


    private OrderItem() { } // EF Core

    public OrderItem(Guid orderId, Guid productId, int quantity, decimal unitPrice)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}