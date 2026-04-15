using DP.Application.Interfaces;
using MediatR;

namespace DP.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _repository;
    private readonly ICacheService _cache;


    public DeleteProductCommandHandler(IProductRepository repository, ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {

        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
            throw new Exception("Product not found");

        await _repository.DeleteAsync(product, cancellationToken);

        // Invalidate cache
        await _cache.RemoveByPrefixAsync("products_");
    }
}