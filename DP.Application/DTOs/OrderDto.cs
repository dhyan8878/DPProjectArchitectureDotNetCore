public class OrderDto
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = default!;
    public decimal TotalAmount { get; set; }
}