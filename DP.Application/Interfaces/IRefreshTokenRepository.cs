using DP.Domain.Entities;

namespace DP.Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token, CancellationToken cancellationToken);
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken);
}