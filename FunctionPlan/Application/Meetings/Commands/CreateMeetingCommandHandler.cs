using MediatR;
using Domain.Common;
using Domain.Meetings;
namespace Application.Meetings.Commands
{
    internal sealed class CreateMeetingCommandHandler: IRequestHandler<CreateMeetingCommand, int>
    {
        private readonly IMeetingRepository _meetingRepository;

        public CreateMeetingCommandHandler(IMeetingRepository meetingRepository)
        {
           _meetingRepository = meetingRepository;
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

            await _meetingRepository.AddAsync( meeting );

            return meeting.Id;
        }
    }
}
