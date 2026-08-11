using MediatR;
using Domain.Common;
using Domain.Meetings;
using Domain.Users;
using Application.Exceptions;
namespace Application.Meetings.Commands
{
    internal sealed class CreateMeetingCommandHandler: IRequestHandler<CreateMeetingCommand, int>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMeetingCommandHandler(IMeetingRepository meetingRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _meetingRepository = meetingRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
           
            //Check if organizer exists
            var Organizer = await _userRepository.GetByIdAsync( request.OrganizerId, cancellationToken);

            if(Organizer is null)
            {
                throw new UserNotFoundException($"Organizer with ID {request.OrganizerId} was not found.");
            }

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
