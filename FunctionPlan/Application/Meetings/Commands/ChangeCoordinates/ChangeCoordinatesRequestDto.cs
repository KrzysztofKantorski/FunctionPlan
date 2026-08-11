namespace Application.Meetings.Commands.ChangeCoordinates
{
    public sealed record ChangeCoordinatesRequestDto(
        double Longitude,
        double Latitude
    );
}
