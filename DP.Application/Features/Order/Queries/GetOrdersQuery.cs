using DP.Application.Common;
using MediatR;

namespace DP.Application.Features.Orders.Queries;

public class GetOrdersQuery : IRequest<PagedResult<OrderDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}