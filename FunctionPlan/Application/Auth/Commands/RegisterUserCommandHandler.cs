using Application.Abstractions.Security;
using Domain.Common;
using Domain.Users;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Auth.Commands
{
    internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            //Check if user with provided credentials alerdy exists
            var existingUser = await _userRepository.GetByEmailAddressAsync(request.Email);

            if (existingUser != null)
            {
                throw new ValidationException("User with provided credentials alerdy exists");
            }

            //Hash user password
            string passwordHash = _passwordHasher.Hash(request.Password);


            //Create user object
            var user = User.CreateWithPassword(
                request.Username,
                request.Email,
                passwordHash
            );


            //Save user to db
            await _userRepository.AddAsync(user, cancellationToken);

            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }
    }
}
