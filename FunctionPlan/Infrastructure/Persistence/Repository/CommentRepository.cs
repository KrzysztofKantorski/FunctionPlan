using Domain.Comments;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    public sealed class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Comment comment, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<Comment>()
                .AddAsync(comment, cancellationToken);
        }

        public async Task<Comment?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<Comment>()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Remove(Comment comment)
        {
            _dbContext.Set<Comment>()
                .Remove(comment);
        }

        public void Update(Comment comment)
        {
            _dbContext.Set<Comment>()
                .Update(comment);
        }
    }
}
