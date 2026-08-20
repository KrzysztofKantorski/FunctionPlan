using FluentValidation;

namespace Application.Comments.Commands.CreateCommentCommand
{
    internal sealed class CreateCommentCommandValidator: AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator() 
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Meeting cannot be empty")
                .GreaterThanOrEqualTo(0).WithMessage("Incorrect meeting Id");

            RuleFor(x => x.AuthorId)
               .NotEmpty().WithMessage("Author cannot by empty")
               .GreaterThanOrEqualTo(0).WithMessage("Incorrect comment author Id");

            RuleFor(x => x.Content)
              .NotEmpty().WithMessage("Comment content cannot by empty")
              .MaximumLength(300).WithMessage("Incorrect comment content");

            When(x => x.ParentCommentId != null, () =>
            {
                RuleFor(x => x.ParentCommentId)
                    .NotEmpty().WithMessage("Parent comment cannot by empty")
                    .GreaterThanOrEqualTo(0).WithMessage("Incorrect parent comment");
            });
        }
    }
}
