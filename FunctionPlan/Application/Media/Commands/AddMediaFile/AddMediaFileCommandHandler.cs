using Application.Abstractions.Storage;
using Application.Exceptions;
using Domain.Common;
using Domain.Media;
using Domain.Meetings;
using Domain.Users;
using MediatR;

namespace Application.Media.Commands.AddMediaFile
{
    internal sealed class AddMediaFileCommandHandler : IRequestHandler<AddMediaFileCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediaRepository _mediaRepository;
        private readonly IBlobService _blobService;

        public AddMediaFileCommandHandler (IUserRepository userRepository, IMeetingRepository meetingRepository, IUnitOfWork unitOfWork,
            IMediaRepository mediaRepository, IBlobService blobService)
        {
            _userRepository = userRepository;
            _meetingRepository = meetingRepository;
            _unitOfWork = unitOfWork;
            _mediaRepository = mediaRepository;
            _blobService = blobService;
        }

        public async Task Handle(AddMediaFileCommand request, CancellationToken cancellationToken)
        {
            var uploader = await _userRepository.GetByIdAsync(request.UploaderId);

            if (uploader == null)
            {
                throw new UserNotFoundException("User not found");
            }

            var meeting = await _meetingRepository.GetByIdAsync(request.MeetingId);

            if (meeting == null)
            {
                throw new MeetingNotFoundException("Meeting not found");
            }

            //Create GUID for azure 
            var fileName = Guid.NewGuid();

            //Call domain metohod
            meeting.AddMedia(uploader, fileName, request.Description);

            //Save file to azure storage
            await _blobService.UploadFileAsync(fileName, request.File.Stream, request.File.ContentType, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
