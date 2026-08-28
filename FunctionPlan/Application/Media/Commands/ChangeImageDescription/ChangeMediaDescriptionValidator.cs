using FluentValidation;

namespace Application.Media.Commands.ChangeImageDescription
{
    public sealed class ChangeMediaDescriptionValidator: AbstractValidator<ChangeMediaDescriptionCommand>
    {
        public ChangeMediaDescriptionValidator() 
        {
            RuleFor(x => x.MeetingId).GreaterThan(0);
            RuleFor(x => x.UserId).GreaterThan(0);

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Too long description.");
        }
    }
}
