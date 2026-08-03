using Application.Abstractions.Security;
using Application.Exceptions;
using Domain.Common;
using Domain.Users;
using MediatR;

namespace Application.Auth.Commands.RegisterUser
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
            //Check if user with provided email alerdy exists
            var existingEmail = await _userRepository.GetByEmailAddressAsync(request.Email);

            if (existingEmail != null)
            {
                throw new ConflictException("User with provided email alerdy exists");
            }

            //Check if user provided unique username
            var existingUsername = await _userRepository.GetByUsernameAsync(request.Username);

            if (existingUsername != null) 
            {
                throw new ConflictException("User with provided username alerdy exists");
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
