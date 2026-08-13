using FluentValidation;

namespace Application.Meetings.Commands.CancelMeetingAttendance.cs
{
    public sealed class CancelMeetingAttendanceCommandValidator: AbstractValidator<CancelMeetingAttendanceCommand>
    {
        public CancelMeetingAttendanceCommandValidator() 
        {
            RuleFor(x => x.MeetingId)
                  .NotEmpty().WithMessage("Meeting id must be provided")
                  .GreaterThanOrEqualTo(0).WithMessage("Incorrect meeting id");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User id must be provided")
                .GreaterThanOrEqualTo(0).WithMessage("Incorrect user id");
        }
    }
}
