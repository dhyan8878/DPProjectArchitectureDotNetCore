using DP.Application.Interfaces;
using MediatR;

namespace DP.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _repository;

    public UpdateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
            throw new Exception("Product not found");

        product.UpdateDetails(request.Name, request.Description, request.Price);

        await _repository.UpdateAsync(product, cancellationToken);
    }
}