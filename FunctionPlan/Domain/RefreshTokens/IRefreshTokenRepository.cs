namespace Domain.RefreshTokens
{
    public interface IRefreshTokenRepository
    {
        //Login / logout
        Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);

        //Logout from all devices
        Task<IEnumerable<RefreshToken>> GetAllByUserIdAsync(int userId, CancellationToken cancellationToken = default);

        Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);

        //Save changes (for ex. when revoking a token)
        void Update(RefreshToken refreshToken);
    }
}
