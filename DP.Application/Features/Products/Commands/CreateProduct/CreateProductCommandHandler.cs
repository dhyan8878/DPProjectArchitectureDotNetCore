using DP.Application.Features.Products.Commands.CreateProduct;
using DP.Application.Interfaces;
using DP.Domain.Entities;
using MediatR;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cache;

    public CreateProductCommandHandler(
        IProductRepository repository,
        IUnitOfWork unitOfWork,
        ICacheService cache)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _cache = cache;
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


        // Invalidate cache
        await _cache.RemoveByPrefixAsync("products_");
        
        return product.Id;
    }
}