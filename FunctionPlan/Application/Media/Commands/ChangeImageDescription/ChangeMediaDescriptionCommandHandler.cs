using Application.Abstractions.Storage;
using Application.Exceptions;
using Domain.Common;
using Domain.Meetings;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Media.Commands.ChangeImageDescription
{
    internal sealed class ChangeMediaDescriptionCommandHandler : IRequestHandler<ChangeMediaDescriptionCommand>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeMediaDescriptionCommandHandler(IMeetingRepository meetingRepository, IBlobService blobService,
            IOptions<BlobSettings> blobOptions, IUnitOfWork unitOfWork)
        {
            _meetingRepository = meetingRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(ChangeMediaDescriptionCommand request, CancellationToken cancellationToken)
        {

            if (!Guid.TryParse(request.ImageId, out Guid fileId))
            {
                throw new ImageNotFound("Incorrect image id format.");
            }

            var meeting = await _meetingRepository.GetByIdWithMediaAsync(request.MeetingId);

            if (meeting == null)
            {
                throw new MeetingNotFoundException("Meeting does not exist");
            }


            meeting.UpdateImageDescription(fileId, request.UserId, request.Description);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
