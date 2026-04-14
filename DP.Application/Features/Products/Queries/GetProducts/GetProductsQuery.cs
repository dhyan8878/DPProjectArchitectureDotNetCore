using DP.Application.Common;
using DP.Application.DTOs;
using MediatR;

namespace DP.Application.Features.Products.Queries.GetProducts;

public class GetProductsQuery : IRequest<PagedResult<ProductDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string? Search { get; set; }

    public string? SortBy { get; set; } = "Name";
    public bool IsDescending { get; set; } = false;
}