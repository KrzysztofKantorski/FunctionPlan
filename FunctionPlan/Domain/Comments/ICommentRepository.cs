namespace Domain.Comments
{
    public interface ICommentRepository
    {
        Task<Comment?> GetByIdAsync(int id, CancellationToken cancellationToken);
        void Update(Comment comment);
        Task AddAsync(Comment comment, CancellationToken cancellationToken = default);
        void Remove(Comment comment);
    }
}
