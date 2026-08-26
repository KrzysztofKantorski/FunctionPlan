namespace Domain.Media
{
    public interface IMediaRepository
    {
        Task<MediaFile?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(MediaFile media, CancellationToken cancellationToken);
        void UpdateAsync(MediaFile media);
        void Remove(MediaFile media);
    }
}
