using Application.Common.Dto;
using FluentValidation;

namespace Application.Common.Validators
{
    public sealed class MeetingFiltersDtoValidator: AbstractValidator<MeetingFiltersDto>
    {
        public MeetingFiltersDtoValidator() 
        {

            //Search term
            RuleFor(x => x.SearchTerm)
                 .MaximumLength(100).WithMessage("Incorrect search phrase");


            //Sorting
            RuleFor(x => x.SortOrder)
                .Must(x => string.IsNullOrWhiteSpace(x) || x.ToLower() == "asc" || x.ToLower() == "desc")
                .WithMessage("Incorrect sort order");



            //Date range
            When(x => x.StartDate != null, () =>
            {
                RuleFor(x => x.StartDate!.Value)
                  .NotEmpty()
                  .WithMessage("Start date cannot be empty.");
            });

            When(x => x.EndDate != null, () =>
            {
                RuleFor(x => x.EndDate!.Value)
                  .NotEmpty()
                  .WithMessage("End date cannot be empty.");
            });



            //Start < End in date range
            RuleFor(x => x)
                .Must(x => !x.StartDate.HasValue || !x.EndDate.HasValue || x.StartDate <= x.EndDate)
                .WithMessage("StartDate cannot be later than EndDate.");
        }
    }
}
