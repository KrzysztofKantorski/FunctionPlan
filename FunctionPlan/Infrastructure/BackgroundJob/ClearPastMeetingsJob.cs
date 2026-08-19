using Quartz;

namespace Infrastructure.BackgroundJob
{
    internal sealed class ClearPastMeetingsJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
