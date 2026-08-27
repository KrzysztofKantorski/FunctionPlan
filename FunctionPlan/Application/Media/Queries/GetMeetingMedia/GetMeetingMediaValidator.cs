using FluentValidation;

namespace Application.Media.Queries.GetMeetingMedia
{
    public sealed class GetMeetingMediaValidator: AbstractValidator<GetMeetingMediaQuery>
    {
        public GetMeetingMediaValidator() 
        {
            RuleFor(x => x.MeetingId)
               .NotEmpty()
               .GreaterThanOrEqualTo(0).WithMessage("Incorrect meeting");

            RuleFor(x => x.UserId)
               .NotEmpty()
               .GreaterThanOrEqualTo(0).WithMessage("Incorrect user");
        }
    }
}
