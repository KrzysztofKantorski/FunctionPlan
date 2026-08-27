using Application.Abstractions.Storage;
using Application.Exceptions;
using Domain.Common;
using Domain.Meetings;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Media.Commands.RemoveMediaFile
{
    internal sealed class RemoveMediaFileCommandHandler : IRequestHandler<RemoveMediaFileCommand>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IBlobService _blobService;
        private readonly BlobSettings _blobSettings;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveMediaFileCommandHandler(IMeetingRepository meetingRepository, IBlobService blobService,
            IOptions<BlobSettings> blobOptions, IUnitOfWork unitOfWork)
        {
            _meetingRepository = meetingRepository;
            _blobService = blobService;
            _blobSettings = blobOptions.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemoveMediaFileCommand request, CancellationToken cancellationToken)
        {

            if (!Guid.TryParse(request.ImageId, out Guid fileId))
            {
                throw new Exception("Incorrect image id format.");
            }

            var meeting = await _meetingRepository.GetByIdWithMediaAsync(request.MeetingId);

            if (meeting == null) 
            {
                throw new MeetingNotFoundException("Meeting does not exist");
            }

            meeting.RemoveMedia(fileId, request.UserId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //Save file to azure storage
            await _blobService.DeleteFileAsync(
                _blobSettings.MeetingsContainerName,
                fileId,
                cancellationToken);

        }
    }
}
