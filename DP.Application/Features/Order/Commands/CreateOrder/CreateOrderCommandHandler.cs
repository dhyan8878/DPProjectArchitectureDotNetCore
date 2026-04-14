using DP.Application.Features.Orders.Commands.CreateOrder;
using DP.Application.Interfaces;
using DP.Domain.Entities;
using MediatR;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IOrderRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerName);

        foreach (var item in request.Items)
        {
            order.AddOrderItem(item.ProductId, item.Quantity, item.UnitPrice);
        }

        await _repository.AddAsync(order, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}