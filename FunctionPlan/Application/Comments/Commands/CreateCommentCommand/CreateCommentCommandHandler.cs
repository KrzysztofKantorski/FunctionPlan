using Application.Exceptions;
using Domain.Comments;
using Domain.Common;
using Domain.Meetings;
using Domain.Users;
using MediatR;

namespace Application.Comments.Commands.CreateCommentCommand
{
    internal sealed class CreateCommentCommandHandler: IRequestHandler<CreateCommentCommand>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCommentCommandHandler(ICommentRepository commentRepository, IUserRepository userRepository,
            IMeetingRepository meetingRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _meetingRepository = meetingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {

            //Check if user exists
            var user = await _userRepository.GetByIdAsync(request.AuthorId);

            if (user == null) 
            {
                throw new UserNotFoundException("User not found");
            }

            var meeting = await _meetingRepository.GetByIdAsync(request.MeetingId);

            if (meeting == null)
            {
                throw new MeetingNotFoundException("Meeting not found");
            }


            if (request.ParentCommentId.HasValue)
            {
                var parentComment = await _commentRepository.GetByIdAsync(request.ParentCommentId.Value, cancellationToken);

                if (parentComment == null) 
                {
                    throw new Exception("Parent comment not found");
                }
            }

            meeting.AddComment(user, request.Content, request.ParentCommentId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
