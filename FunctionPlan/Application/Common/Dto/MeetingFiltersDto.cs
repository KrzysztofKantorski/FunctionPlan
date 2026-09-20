namespace Application.Common.Dto
{
    public sealed record MeetingFiltersDto(
        string? SearchTerm,
        string? SortOrder,
        DateTime? StartDate,
        DateTime? EndDate
    );
}
