using Application.Exceptions;
using Domain.Common;
using Domain.Meetings;
using Domain.Users;
using MediatR;

namespace Application.Meetings.Commands.CancelMeetingAttendance.cs
{
    internal sealed class CancelMeetingAttendanceCommandHandler : IRequestHandler<CancelMeetingAttendanceCommand>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CancelMeetingAttendanceCommandHandler(IMeetingRepository meetingRepository, IUserRepository userRepository,
            IUnitOfWork unitOfWork) 
        { 
            _meetingRepository= meetingRepository;
            _userRepository= userRepository;
            _unitOfWork= unitOfWork;
        }


        public async Task Handle(CancelMeetingAttendanceCommand request, CancellationToken cancellationToken)
        {
            var attendee = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (attendee == null) 
            {
                throw new UserNotFoundException("User not found");
            }

            var meeting = await _meetingRepository.GetByIdWithUsersAsync(request.MeetingId, cancellationToken);

            if (meeting == null) 
            {
                throw new MeetingNotFoundException("Invalid meeting");
            }

            meeting.CancelAttendence(attendee);

            _meetingRepository.Update(meeting);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
