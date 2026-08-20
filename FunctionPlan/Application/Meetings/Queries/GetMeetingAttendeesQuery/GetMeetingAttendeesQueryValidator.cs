using FluentValidation;


namespace Application.Meetings.Queries.GetMeetingAttendeesQuery
{
    internal sealed class GetMeetingAttendeesQueryValidator :AbstractValidator<GetMeetingAttendeesQuery>
    {
        public GetMeetingAttendeesQueryValidator() 
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1).WithMessage("Incorrect meeting");
        }
    }
}
