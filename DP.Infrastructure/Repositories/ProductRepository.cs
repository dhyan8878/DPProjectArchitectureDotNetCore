using DP.Application.Interfaces;
using DP.Domain.Entities;
using DP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DP.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string? search,
        string? sortBy,
        bool isDescending,
        CancellationToken cancellationToken)
    {
        var query = _context.Products.AsQueryable();

        // 🔍 Filtering
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x => x.Name.Contains(search));
        }

        // 🔽 Sorting
        query = sortBy?.ToLower() switch
        {
            "price" => isDescending ? query.OrderByDescending(x => x.Price) : query.OrderBy(x => x.Price),
            "name" => isDescending ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name),
            _ => query.OrderBy(x => x.Name)
        };

        // 📊 Total Count
        var totalCount = await query.CountAsync(cancellationToken);

        // 📄 Paging
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }
}