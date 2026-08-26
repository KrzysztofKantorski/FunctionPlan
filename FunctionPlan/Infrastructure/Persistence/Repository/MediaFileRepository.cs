using Domain.Media;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    internal sealed class MediaFileRepository : IMediaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MediaFileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(MediaFile media, CancellationToken cancellationToken)
        {
            await _dbContext.Set<MediaFile>()
                .AddAsync(media, cancellationToken);
        }


        public async Task<MediaFile?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<MediaFile>()
                .FirstOrDefaultAsync(x=> x.Id == id, cancellationToken);
        }


        public void Remove(MediaFile media)
        {
            _dbContext.Set<MediaFile>()
                .Remove(media);
        }


        public void UpdateAsync(MediaFile media)
        {
            _dbContext.Set<MediaFile>()
               .Update(media);
        }

    }
}
