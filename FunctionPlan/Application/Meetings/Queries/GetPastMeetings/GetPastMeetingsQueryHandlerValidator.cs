using FluentValidation;


namespace Application.Meetings.Queries.GetPastMeetings
{
    internal sealed class GetPastMeetingsQueryHandlerValidator: AbstractValidator<GetPastMeetingsQuery>
    {
        public GetPastMeetingsQueryHandlerValidator() 
        {
            RuleFor(x => x.SearchTerm)
                .MaximumLength(100).WithMessage("Incorrect search phrase");

            RuleFor(x => x.SortOrder)
                .Must(x => string.IsNullOrWhiteSpace(x) || x.ToLower() == "asc" || x.ToLower() == "desc")
                .WithMessage("Incorrect sort order");

            When(x => x.Status != null, () =>
            {
                RuleFor(x => x.Status)
                    .IsInEnum().WithMessage("Incorrect meeting status.");
            });
        }
    }
}
