using Application.Common.Validators;
using FluentValidation;

namespace Application.Users.Commands.UploadUserImage
{
    public sealed class UploadUserImageValidator: AbstractValidator<UploadUserImageCommand>
    {

        public UploadUserImageValidator()
        {
            RuleFor(x => x.UserId)
                 .GreaterThanOrEqualTo(0)
                 .WithMessage("Incorrect user id");

            RuleFor(x => x.UploadedImage)
                .NotNull()
                .WithMessage("File is required")
                .SetValidator(new ImageFileDtoValidator());

        }
    }
}
