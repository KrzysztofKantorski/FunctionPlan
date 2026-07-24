using MediatR;
using Domain.Common;
using Domain.Meetings;
namespace Application.Meetings.Commands
{
    internal sealed class CreateMeetingCommandHandler: IRequestHandler<CreateMeetingCommand, int>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMeetingCommandHandler(IMeetingRepository meetingRepository, IUnitOfWork unitOfWork)
        {
            _meetingRepository = meetingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            var location = new Coordinates(request.Latitude, request.Longitude);

            var meeting = new Meeting(
                request.Title,
                request.ScheduledFor,
                request.OrganizerId,
                location
            );

            await _meetingRepository.AddAsync(meeting, cancellationToken);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return meeting.Id;
        }
    }
}
