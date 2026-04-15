using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
