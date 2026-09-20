using Application.Common.Validators;
using Application.Meetings.Queries.GetAttendeedMeetings;
using FluentValidation;

namespace Application.Meetings.Queries.GetOrganizedMeetings
{
    // Zakładam, że powinieneś walidować zapytanie GetOrganizedMeetingsQuery, a nie sam validator.
    internal sealed class GetOrganizedMeetingsValidator : AbstractValidator<GetOrganizedMeetingsQuery>
    {
        public GetOrganizedMeetingsValidator()
        {
            RuleFor(x => x.Filters)
                 .SetValidator(new MeetingFiltersDtoValidator());
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("Invalid user id");
        }
    }
}
