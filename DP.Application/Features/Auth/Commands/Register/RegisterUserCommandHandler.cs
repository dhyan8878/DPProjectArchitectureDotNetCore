using DP.Application.Interfaces;
using DP.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _hasher;

        public RegisterUserCommandHandler(
            IUserRepository repository,
            IUnitOfWork unitOfWork,
            IPasswordHasher hasher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _hasher = hasher;
        }


        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _repository.GetByEmailAsync(request.Email, cancellationToken);

            if (existingUser != null)
                throw new Exception("User already exists");

            var passwordHash = _hasher.Hash(request.Password);

            var user = new User(request.Email, passwordHash);

            await _repository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
