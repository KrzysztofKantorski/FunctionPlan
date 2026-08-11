using FluentValidation;

namespace Application.Meetings.Commands.RescheduleMeetingCommand
{
    public sealed class RescheduleMeetingCommandValidator: AbstractValidator<RescheduleMeetingCommand>
    {
        public RescheduleMeetingCommandValidator()
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Meeting id must be provided")
                .GreaterThanOrEqualTo(0).WithMessage("Incorrect meeting id");

            RuleFor(x => x.OrganizerId)
                .NotEmpty().WithMessage("User id must be provided")
                .GreaterThanOrEqualTo(0).WithMessage("Incorrect user id");

            RuleFor(x => x.ScheduledFor)
               .GreaterThan(DateTime.UtcNow).WithMessage("Incorrect meeting date");
        }
    }
}
