using Application.Abstractions.Messaging;

namespace Application.Meetings.Commands.ChangeCoordinates
{
    public sealed record ChangeCoordinatesCommand(
        int MeetingId,
        int OrganizerId,
        double Longitude,
        double Latitude
    ): ICommand;
        
}
