using FluentValidation;

namespace Application.Meetings.Commands
{
    public sealed class CreateMeetingCommandValidator: AbstractValidator<CreateMeetingCommand>
    {
        public CreateMeetingCommandValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Meeting title cannot be empty")
                .MaximumLength(100).WithMessage("Incorrect title content");

            RuleFor(x => x.ScheduledFor)
                .GreaterThan(DateTime.UtcNow).WithMessage("Incorrect meeting date");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("Incorrect Latitude");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Incorrect Longitude");
        }
    }
}
