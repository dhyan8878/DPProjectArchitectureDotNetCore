using DP.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Features.Auth.Commands.RefreshTokens
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, string>
    {
        private readonly IRefreshTokenRepository _repo;
        private readonly IJwtTokenGenerator _jwt;

        public RefreshTokenHandler(IRefreshTokenRepository repo, IJwtTokenGenerator jwt)
        {
            _repo = repo;
            _jwt = jwt;
        }

        public async Task<string> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await _repo.GetByTokenAsync(request.RefreshToken, cancellationToken);

            if (token == null || token.IsRevoked || token.ExpiryDate < DateTime.UtcNow)
                throw new Exception("Invalid refresh token");

            return _jwt.GenerateToken(token.UserId.ToString(), "", "User");
        }
    }
}
