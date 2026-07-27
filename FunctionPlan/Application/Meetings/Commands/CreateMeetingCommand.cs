using Application.Abstractions.Messaging;
using MediatR;

namespace Application.Meetings.Commands
{
    public sealed record CreateMeetingCommand(
        string Title,
        DateTime ScheduledFor,
        int OrganizerId,
        double Latitude,
        double Longitude
    ): ICommand<int>;

}
