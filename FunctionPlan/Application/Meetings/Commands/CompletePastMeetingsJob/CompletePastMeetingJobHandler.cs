using Domain.Common;
using Domain.Meetings;
using MediatR;

namespace Application.Meetings.Commands.CompletePastMeetingsJob
{
    internal sealed class CompletePastMeetingJobHandler : IRequestHandler<CompletePastMeetingJob>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompletePastMeetingJobHandler(IMeetingRepository meetingRepository, IUnitOfWork unitOfWork)
        {
            _meetingRepository = meetingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CompletePastMeetingJob request, CancellationToken cancellationToken)
        {

            var cutoffTime = DateTime.UtcNow.AddHours(-2);

            //Check if there are meetings to mark as completed
            var meetingsToComplete = await _meetingRepository.GetUncompletedPastMeetings(cutoffTime, cancellationToken);

            //There are no meetings to complete
            if (!meetingsToComplete.Any()) 
            {
                return;
            }

            //Complete meetings
            foreach(var meeting in meetingsToComplete)
            {
                meeting.MarkAsCompleted();
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
