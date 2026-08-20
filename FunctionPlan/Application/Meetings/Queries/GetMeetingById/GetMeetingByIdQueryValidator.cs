using FluentValidation;


namespace Application.Meetings.Queries.GetMeetingById
{
    internal sealed class GetMeetingByIdQueryValidator: AbstractValidator<GetMeetingByIdQuery>
    {
        public GetMeetingByIdQueryValidator() 
        {
            RuleFor(x => x.MeetingId)
               .NotEmpty()
               .GreaterThanOrEqualTo(1).WithMessage("Incorrect meeting");
        }
    }
}
