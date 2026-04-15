using DP.Application.Common;
using DP.Application.Features.Auth.Commands.Login;
using DP.Application.Interfaces;
using DP.Domain.Entities;
using MediatR;

namespace DP.Application.Features.Auth.Commands.Login;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponse>
{
    private readonly IUserRepository _repository;
    private readonly IJwtTokenGenerator _jwt;
    private readonly IPasswordHasher _hasher;
    private readonly IRefreshTokenRepository _refreshRepo;
    private readonly IUnitOfWork _unitOfWork;
    public LoginUserCommandHandler(IUserRepository repository, IJwtTokenGenerator jwt, IPasswordHasher hasher,
                        IRefreshTokenRepository refreshRepo,IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _jwt = jwt;
        _hasher = hasher;
        _refreshRepo = refreshRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByEmailAsync(request.Email, cancellationToken);

        if (user == null || !_hasher.Verify(request.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        var accessToken = _jwt.GenerateToken(user.Id.ToString(), user.Email, user.Role);

        var refreshTokenValue = TokenGenerator.GenerateRefreshToken();

        var refreshToken = new RefreshToken(
            user.Id,
            refreshTokenValue,
            DateTime.UtcNow.AddDays(7));

        await _refreshRepo.AddAsync(refreshToken, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue
        };
    }

}