using FluentValidation;

namespace Application.Comments.Queries.GetMeetingCommentsQuery
{
    internal sealed class GetMeetingCommentsQueryValidator: AbstractValidator<GetMeetingCommentsQuery>
    {
        public GetMeetingCommentsQueryValidator()
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Meeting cannot be empty")
                .GreaterThanOrEqualTo(0).WithMessage("Incorrect Meeting");
        }
    }
}
