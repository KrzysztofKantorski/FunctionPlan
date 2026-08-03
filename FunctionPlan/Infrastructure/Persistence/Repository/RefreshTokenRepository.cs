

using Domain.RefreshTokens;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Persistence.Repository
{
    internal class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RefreshTokenRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<RefreshToken>().AddAsync(refreshToken, cancellationToken);
        }

        public async Task<IEnumerable<RefreshToken>> GetAllByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<RefreshToken>()
                .Where(rt => rt.UserId == userId && !rt.IsRevoked)
                .ToListAsync(cancellationToken);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<RefreshToken>()
                .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
        }

        public void Update(RefreshToken refreshToken)
        {
            _dbContext.Set<RefreshToken>().Update(refreshToken);
        }
    }
}
