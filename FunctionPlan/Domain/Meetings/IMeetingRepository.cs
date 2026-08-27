namespace Domain.Meetings
{
    public interface IMeetingRepository
    {
        //Get meetings by title
        Task<IEnumerable<Meeting>> GetByTitleAsync(string title, CancellationToken cancellationToken = default);

        //Display info about specific meeting
        Task<Meeting?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        //Get meeting with users
        Task<Meeting?> GetByIdWithUsersAsync(int id, CancellationToken cancellationToken = default);

        //Get meeting with images
        Task<Meeting?> GetByIdWithMediaAsync(int id, CancellationToken cancellationToken = default);

        //Get meetings to mark as completed
        Task<List<Meeting>> GetUncompletedPastMeetings(DateTime referenceDate, CancellationToken cancellationToken);

        Task AddAsync(Meeting meeting, CancellationToken cancellationToken = default);

        void Update(Meeting meeting);

        void Remove(Meeting meeting);
    }
}
