using Application.Common.Validators;
using FluentValidation;

namespace Application.Meetings.Queries.GetMeetings
{
    internal sealed class GetMeetingsQueryHandlerValidator: AbstractValidator<GetMeetingsQuery>
    {
        public GetMeetingsQueryHandlerValidator() 
        {
            RuleFor(x => x.Filters)
                 .SetValidator(new MeetingFiltersDtoValidator());
        }
    }
}
