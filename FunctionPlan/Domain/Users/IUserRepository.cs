namespace Domain.Users
{
    public interface IUserRepository
    {
        //Register user
        Task AddAsync(User user, CancellationToken cancellationToken = default);

        //Get user by email address (email + password auth)
        Task<User?> GetByEmailAddressAsync(string email, CancellationToken cancellationToken = default);

        //Get user by google id
        Task<User?> GetByGoogleSubjectIdAsync(string googleSubjectId, CancellationToken cancellationToken = default);

        //Get user by id
        Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
