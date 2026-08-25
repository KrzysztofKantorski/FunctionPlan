using FluentValidation;


namespace Application.Users.Queries.GetUserImageQuery
{
    public sealed class GetUserImageQueryValidator: AbstractValidator<GetUserImageQuery>
    {
        public GetUserImageQueryValidator() {

            RuleFor(x => x.UserId)
                .GreaterThanOrEqualTo(0).WithMessage("Incorrect user id");
        }
    }
}
