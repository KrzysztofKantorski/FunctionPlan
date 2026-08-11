using FluentValidation;
namespace Application.Meetings.Commands.CancellMeetingCommand
{
    public sealed class CancelMeetingCommandValidator: AbstractValidator<CancelMeetingCommand>
    {
        public CancelMeetingCommandValidator() 
        {
            RuleFor(x => x.MeetingId)
                    .NotEmpty().WithMessage("Meeting id must be provided")
                    .GreaterThanOrEqualTo(0).WithMessage("Incorrect meeting id");

            RuleFor(x => x.OrganizerId)
                .NotEmpty().WithMessage("User id must be provided")
                .GreaterThanOrEqualTo(0).WithMessage("Incorrect user id");
        }
    }
}
