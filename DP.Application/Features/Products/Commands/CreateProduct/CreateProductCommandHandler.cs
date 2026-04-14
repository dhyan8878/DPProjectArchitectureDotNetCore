using DP.Application.Features.Products.Commands.CreateProduct;
using DP.Application.Interfaces;
using DP.Domain.Entities;
using MediatR;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            request.Name,
            request.Description,
            request.Price,
            request.StockQuantity);

        await _repository.AddAsync(product, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}