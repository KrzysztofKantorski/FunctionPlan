using Domain.Common;
using Domain.Meetings;
using MediatR;

namespace Application.Meetings.Commands.RescheduleMeetingCommand
{
    internal sealed class RescheduleMeetingCommandHandler: IRequestHandler<RescheduleMeetingCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMeetingRepository _meetingRepository;

        public RescheduleMeetingCommandHandler(IUnitOfWork unitOfWork, IMeetingRepository meetingRepository) 
        { 
            _unitOfWork = unitOfWork;
            _meetingRepository = meetingRepository;
        }

        public async Task Handle(RescheduleMeetingCommand request, CancellationToken cancellationToken)
        {

            //Check if user is organizer
            var meetingToReschedule = await _meetingRepository.GetByIdAsync(request.MeetingId, cancellationToken);

            if (meetingToReschedule is null) 
            {
                throw new Exception("Meeting not found");
            }

            //Check if user is organizer
            int meetingOrganizer = meetingToReschedule.OrganizerId;

            if(request.OrganizerId != meetingOrganizer)
            {
                throw new Exception("You dont have permision to reschedule meeting");
            }

            //Check date
            var scheduledForUtc = request.ScheduledFor.Kind == DateTimeKind.Utc
               ? request.ScheduledFor
               : request.ScheduledFor.ToUniversalTime();

            //Reschedule
            meetingToReschedule.Reschedule(scheduledForUtc);

            _meetingRepository.Update(meetingToReschedule);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
