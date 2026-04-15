using DP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DP.WebAPI.Controllers;

using DP.Application.Features.Auth.Commands.Login;
using DP.Application.Features.Auth.Commands.RefreshTokens;
using DP.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var token = await _mediator.Send(command);
        return Ok(new { Token = token });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenCommand command)
    {
        var token = await _mediator.Send(command);
        return Ok(new { AccessToken = token });
    }
}

public class LoginRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}