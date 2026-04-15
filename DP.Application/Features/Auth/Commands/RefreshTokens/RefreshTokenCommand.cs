using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Features.Auth.Commands.RefreshTokens
{
    public class RefreshTokenCommand : IRequest<string>
    {
        public string RefreshToken { get; set; } = default!;
    }
}
