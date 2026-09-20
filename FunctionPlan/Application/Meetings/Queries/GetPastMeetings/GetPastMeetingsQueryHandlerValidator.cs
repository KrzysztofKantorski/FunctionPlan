using Application.Common.Validators;
using FluentValidation;


namespace Application.Meetings.Queries.GetPastMeetings
{
    internal sealed class GetPastMeetingsQueryHandlerValidator: AbstractValidator<GetPastMeetingsQuery>
    {
        public GetPastMeetingsQueryHandlerValidator() 
        {
            RuleFor(x => x.Filters)
                .SetValidator(new MeetingFiltersDtoValidator());
        }
    }
}
