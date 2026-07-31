using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    internal class AuthRepository: IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthRepository(ApplicationDbContext dbCcontext)
        {
            _dbContext = dbCcontext;
        }


        public async Task<User?> GetByEmailAddressAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<User>()
                .FirstOrDefaultAsync(x=> x.Email == email, cancellationToken);
        }


        public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<User>()
                .FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
        }


        public async Task<User?> GetByGoogleSubjectIdAsync(string googleSubjectId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<User>()
                .FirstOrDefaultAsync(x=> x.GoogleSubjectId == googleSubjectId, cancellationToken);
        }


        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<User>()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }


        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<User>()
                .AddAsync(user, cancellationToken);
        }


    }
}
