using FluentValidation;

namespace Application.Meetings.Commands.ChangeCoordinates
{
    public sealed class ChangeCoordinatesCommandValidator: AbstractValidator<ChangeCoordinatesCommand>
    {
        public ChangeCoordinatesCommandValidator() 
        {
            RuleFor(x => x.MeetingId)
               .NotEmpty().WithMessage("Meeting id must be provided")
               .GreaterThanOrEqualTo(0).WithMessage("Incorrect meeting id");

            RuleFor(x => x.OrganizerId)
                .NotEmpty().WithMessage("User id must be provided")
                .GreaterThanOrEqualTo(0).WithMessage("Incorrect user id");

            RuleFor(x => x.Latitude)
               .InclusiveBetween(-90, 90).WithMessage("Incorrect Latitude");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Incorrect Longitude");
        }
    }
}
