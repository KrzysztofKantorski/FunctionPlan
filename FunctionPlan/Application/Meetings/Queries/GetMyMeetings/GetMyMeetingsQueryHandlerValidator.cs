using Application.Common.Validators;
using FluentValidation;

namespace Application.Meetings.Queries.GetMyMeetings
{
    internal sealed class GetMyMeetingsQueryHandlerValidator: AbstractValidator<GetMyMeetingsQuery>
    {
        public GetMyMeetingsQueryHandlerValidator() 
        {
            RuleFor(x => x.Filters)
                 .SetValidator(new MeetingFiltersDtoValidator());
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("Invalid user id");
        }
    }
}
