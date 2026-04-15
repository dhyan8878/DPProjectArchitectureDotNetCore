using DP.Application.Interfaces;
using MediatR;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _repository;
    private readonly IJwtTokenGenerator _jwt;
    private readonly IPasswordHasher _hasher;
    public LoginUserCommandHandler(IUserRepository repository, IJwtTokenGenerator jwt, IPasswordHasher hasher)
    {
        _repository = repository;
        _jwt = jwt;
        _hasher = hasher;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByEmailAsync(request.Email, cancellationToken);

        if (user == null || !_hasher.Verify(request.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        return _jwt.GenerateToken(user.Id.ToString(), user.Email, user.Role);
    }
}