namespace Domain.Media
{
    public interface IMediaRepository
    {
        Task<Media?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(Media media, CancellationToken cancellationToken);
        void UpdateAsync(Media media);
        void Remove(Media media);
    }
}
