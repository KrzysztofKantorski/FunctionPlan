using FluentValidation;


namespace Application.Media.Queries.GetUsersAvatars
{
    public sealed class GetUsersAvatarsValidator: AbstractValidator<GetUsersAvatarsQuery>
    {

        public GetUsersAvatarsValidator() 
        {
            RuleFor(x => x.UserId)
               .NotEmpty()
               .GreaterThanOrEqualTo(0).WithMessage("Incorrect user");

            RuleFor(x => x.ImageId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("Incorrect image id");
        }
    }
}
