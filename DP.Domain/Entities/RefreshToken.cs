using DP.Domain.Common;

namespace DP.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public Guid UserId { get; private set; }
    public string Token { get; private set; } = default!;
    public DateTime ExpiryDate { get; private set; }
    public bool IsRevoked { get; private set; }

    private RefreshToken() { }

    public RefreshToken(Guid userId, string token, DateTime expiryDate)
    {
        UserId = userId;
        Token = token;
        ExpiryDate = expiryDate;
        IsRevoked = false;
    }

    public void Revoke()
    {
        IsRevoked = true;
    }
}