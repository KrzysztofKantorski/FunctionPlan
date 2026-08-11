using Application.Exceptions;
using Domain.Common;
using Domain.Meetings;
using MediatR;

namespace Application.Meetings.Commands.CancellMeetingCommand
{
    internal sealed class CancelMeetingCommandHandler : IRequestHandler<CancelMeetingCommand>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CancelMeetingCommandHandler(IMeetingRepository meetingRepository, IUnitOfWork unitOfWork) 
        {
            _meetingRepository = meetingRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(CancelMeetingCommand request, CancellationToken cancellationToken)
        {
            var meetingToCancel = await _meetingRepository.GetByIdAsync(request.MeetingId);

            //Check if meeting exists
            if (meetingToCancel is null)
            {
                throw new MeetingNotFoundException("Meeting not found");
            }

            //Check if user is organizer
            if (meetingToCancel.OrganizerId != request.OrganizerId) 
            {
                throw new UserWithoutPermisionException("You dont have permision to reschedule meeting");
            }

            meetingToCancel.Cancel();

            _meetingRepository.Update(meetingToCancel);

            await _unitOfWork.SaveChangesAsync();

        }
    }
}
