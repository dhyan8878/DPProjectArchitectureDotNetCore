using DP.Application.Interfaces;
using MediatR;

namespace DP.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _repository;
    private readonly ICacheService _cache;

    public UpdateProductCommandHandler(IProductRepository repository, ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {

        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
            throw new Exception("Product not found");

        product.UpdateDetails(request.Name, request.Description, request.Price);

        await _repository.UpdateAsync(product, cancellationToken);

        // Invalidate cache
        await _cache.RemoveByPrefixAsync("products_");
    }
}