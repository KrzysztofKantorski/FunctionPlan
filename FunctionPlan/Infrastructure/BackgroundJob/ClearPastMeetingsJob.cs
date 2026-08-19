using MediatR;
using Quartz;
using Application.Meetings.Commands.CompletePastMeetingsJob;
namespace Infrastructure.BackgroundJob
{
    internal sealed class ClearPastMeetingsJob : IJob
    {
        private readonly ISender _sender;
        public ClearPastMeetingsJob(ISender sender)
        {
            _sender = sender;
        }
        public Task Execute(IJobExecutionContext context)
        {
            //Complete past meetings
            _sender.Send(new CompletePastMeetingJob(), context.CancellationToken);

            return Task.CompletedTask;
        }
    }
}
