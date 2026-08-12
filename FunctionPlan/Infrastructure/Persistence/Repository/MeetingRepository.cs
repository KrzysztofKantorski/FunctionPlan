using Domain.Meetings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    public sealed class MeetingRepository: IMeetingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MeetingRepository(ApplicationDbContext dbCcontext) 
        {
            _dbContext = dbCcontext;
        }

        public async Task<IEnumerable<Meeting>> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Meeting>()
                .Where(t => t.Title == title)
                .ToListAsync(cancellationToken);
        }


        public async Task<Meeting?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Meeting>()
                 .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }
        public async Task<Meeting?> GetByIdWithUsersAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Meeting>()
                .Include(m => m.Users)
                .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }
       
        public async Task AddAsync(Meeting meeting, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<Meeting>()
                .AddAsync(meeting, cancellationToken);
        }

        public void Update(Meeting meeting)
        {
            _dbContext.Set<Meeting>().Update(meeting);
        }


        public void Remove(Meeting meeting)
        {
            _dbContext.Set<Meeting>().Remove(meeting);
        }
    }
}
