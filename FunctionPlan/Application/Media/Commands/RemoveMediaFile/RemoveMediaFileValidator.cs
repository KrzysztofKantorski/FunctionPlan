using FluentValidation;

namespace Application.Media.Commands.RemoveMediaFile
{
    public sealed class RemoveMediaFileValidator: AbstractValidator<RemoveMediaFileCommand>
    {
        public RemoveMediaFileValidator() 
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
