using Application.Exceptions;
using Domain.Common;
using Domain.Meetings;
using Domain.Users;
using MediatR;

namespace Application.Meetings.Commands.ConfirmAttendenceCommand
{
    internal sealed class ConfirmAttendenceCommandHandler : IRequestHandler<ConfirmAttendenceCommand>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmAttendenceCommandHandler(IMeetingRepository meetingRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _meetingRepository = meetingRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ConfirmAttendenceCommand request, CancellationToken cancellationToken)
        {

            var userToJoin = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (userToJoin == null) 
            {
                throw new UserNotFoundException("User not found");
            }

            var meetingToJoin = await _meetingRepository.GetByIdAsync(request.MeetingId, cancellationToken);

            if (meetingToJoin == null) 
            {
                throw new MeetingNotFoundException("Meeting not found");
            }

            meetingToJoin.ConfirmAttendence(userToJoin);

            _meetingRepository.Update(meetingToJoin);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }

       
    }
}
