namespace Domain.Users
{
    public interface IUserRepository
    {
        //Register user
        Task AddAsync(User user, CancellationToken cancellationToken = default);

        //Get user by email address (email + password auth)
        Task<User?> GetByEmailAddressAsync(string email, CancellationToken cancellationToken = default);

        //Get user by id
        Task<User?> GetByGoogleIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
