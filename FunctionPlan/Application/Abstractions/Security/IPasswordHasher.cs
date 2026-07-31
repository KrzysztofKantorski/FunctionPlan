namespace Application.Abstractions.Security
{
    public interface IPasswordHasher
    {
        string HasPassword(string password);

        bool Verify(string password, string passwordHash);
    }
}
