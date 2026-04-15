using DP.Application.Common;
using DP.Application.DTOs;
using DP.Application.Interfaces;
using MediatR;

namespace DP.Application.Features.Products.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedResult<ProductDto>>
{
    private readonly IProductRepository _repository;
    private readonly ICacheService _cache;
    public GetProductsQueryHandler(IProductRepository repository, ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<PagedResult<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"products_{request.PageNumber}_{request.PageSize}_{request.Search}_{request.SortBy}_{request.IsDescending}";

        var cached = await _cache.GetAsync<PagedResult<ProductDto>>(cacheKey);

        if (cached != null)
            return cached;

        var (items, totalCount) = await _repository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            request.Search,
            request.SortBy,
            request.IsDescending,
            cancellationToken);

        var mapped = items.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            StockQuantity = p.StockQuantity
        });

        var result = new PagedResult<ProductDto>
        {
            Items = mapped,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        await _cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;
    }
}