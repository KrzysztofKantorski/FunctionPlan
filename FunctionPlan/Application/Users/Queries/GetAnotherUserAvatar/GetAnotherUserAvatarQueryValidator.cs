using FluentValidation;

namespace Application.Users.Queries.GetAnotherUserAvatar
{
    public sealed class GetAnotherUserAvatarQueryValidator: AbstractValidator<GetAnotherUserAvatarQuery>
    {
        public GetAnotherUserAvatarQueryValidator() 
        {
            RuleFor(x => x.userId)
                .GreaterThanOrEqualTo(0).WithMessage("Incorrect user id");
        }
    }
}
