using DP.Application.Interfaces;
using DP.Domain.Entities;
using MediatR;

namespace DP.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _repository;

    public CreateOrderCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerName);

        foreach (var item in request.Items)
        {
            order.AddOrderItem(item.ProductId, item.Quantity, item.UnitPrice);
        }

        await _repository.AddAsync(order, cancellationToken);

        return order.Id;
    }
}