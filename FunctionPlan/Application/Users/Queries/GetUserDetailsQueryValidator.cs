using FluentValidation;

namespace Application.Users.Queries
{
    public sealed class GetUserDetailsQueryValidator: AbstractValidator<GetUserDetailsQuery>
    {
        public GetUserDetailsQueryValidator() 
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .GreaterThanOrEqualTo(0).WithMessage("Incorrect user id");
        }
    }
}
