using DP.Domain.Common;

namespace DP.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string Role { get; private set; } = "User";

    private User() { } // EF

    public User(string email, string passwordHash, string role = "User")
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }
}