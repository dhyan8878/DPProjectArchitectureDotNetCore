using DP.Application.Features.Auth.Commands.Login;
using MediatR;

namespace DP.Application.Features.Auth.Commands.Login;

public class LoginUserCommand : IRequest<LoginResponse>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}