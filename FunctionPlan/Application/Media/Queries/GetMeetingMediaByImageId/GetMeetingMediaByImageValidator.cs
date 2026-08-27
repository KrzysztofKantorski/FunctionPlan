using FluentValidation;

namespace Application.Media.Queries.GetMeetingMediaByImageId
{
    public sealed class GetMeetingMediaByImageValidator: AbstractValidator<GetMeetingMediaByImageQuery>
    {
        public GetMeetingMediaByImageValidator() 
        {
            RuleFor(x => x.MeetingId)
               .NotEmpty()
               .GreaterThanOrEqualTo(0).WithMessage("Incorrect meeting");

            RuleFor(x => x.UserId)
               .NotEmpty()
               .GreaterThanOrEqualTo(0).WithMessage("Incorrect user");

            RuleFor(x => x.ImageId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("Incorrect image id");
        }
    }
}
