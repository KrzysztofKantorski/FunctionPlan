namespace Domain.Meetings
{
    public interface IMeetingRepository
    {
        //Get meetings by title
        Task<IEnumerable<Meeting>> GetByTitleAsync(string title, CancellationToken cancellationToken = default);

        //Display info about specific meeting
        Task<Meeting> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task AddAsync(Meeting meeting, CancellationToken cancellationToken = default);

        void Update(Meeting meeting);

        void Remove(Meeting meeting);
    }
}
