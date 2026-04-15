using DP.Application.Interfaces;
using DP.Domain.Entities;
using DP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DP.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RefreshToken token, CancellationToken cancellationToken)
    {
        await _context.RefreshTokens.AddAsync(token, cancellationToken);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == token, cancellationToken);
    }
}