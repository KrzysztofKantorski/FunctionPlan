using Application.Common.Validators;
using FluentValidation;


namespace Application.Media.Commands.AddMediaFile
{
    public sealed class AddMediaFileCommandValidator: AbstractValidator<AddMediaFileCommand>
    {
        public AddMediaFileCommandValidator() 
        {
            RuleFor(x => x.MeetingId).GreaterThan(0);
            RuleFor(x => x.UploaderId).GreaterThan(0);

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Too long description.");

            RuleFor(x => x.File)
                .NotNull().WithMessage("File is required")
                .SetValidator(new ImageFileDtoValidator()!);
        }
    }
}
