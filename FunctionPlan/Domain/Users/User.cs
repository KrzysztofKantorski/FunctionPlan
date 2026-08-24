using Domain.Comments;
using Domain.Common;
using Domain.Meetings;
using System.Text.RegularExpressions;

namespace Domain.Users
{
    public enum UserRole
    {
        Admin,
        User
    }

    public sealed class User: Entity
    {
        public string Username { get; private set; } = string.Empty;
        public string? PasswordHash { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public UserRole Role { get; private set; }
        public DateTime Created { get; private set; }
        public bool IsBanned { get; private set; }
        public string? ProfilePictureUrl { get; private set; } 
        public string? GoogleSubjectId { get; private set; }
        public bool IsVerified { get; private set; }
        private User() { }

        private readonly List<Meeting> _meetings = new();
        public IReadOnlyCollection<Meeting> Meetings => _meetings.AsReadOnly();

        private readonly List<Comment> _comments = new();
        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

        private User(string username, string email, UserRole role, string? passwordHash, string? googleSubjectId)
        {

            if (username.Length < 3 || username.Length > 30)
            {
                throw new InvalidUserDataException("Incorrect username");
            }

            if(string.IsNullOrWhiteSpace(passwordHash) && string.IsNullOrWhiteSpace(googleSubjectId))
            {
                throw new InvalidUserCredentialsException("You must provide authentication method");
            }

            Username = username;
            PasswordHash = passwordHash;
            Email = email;
            Role = role;
            Created = DateTime.UtcNow;
            IsBanned = false;
            GoogleSubjectId = googleSubjectId;
            IsVerified = false;
        }

        //Check if user verified email address 
        public void verify()
        {
            if (IsVerified)
            {
                throw new InvalidUserCredentialsException("Incorrect user credentials");
            }

            IsVerified = true;
        }


        //Method for local registration (email + password)
        public static User CreateWithPassword(string username, string email, string passwordHash, UserRole role = UserRole.User)
        {
            if(string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new InvalidUserCredentialsException("Incorrect email address");
            }

            if(string.IsNullOrWhiteSpace(passwordHash) || passwordHash.Length < 5)
            {
                throw new InvalidUserCredentialsException("Invalid password");
            }

            var NewUser = new User(username, email, role, passwordHash, null);

            return NewUser;
        }



        //Method for google oauth registration
        public static User CreateWithGoogle(string username, string email, string googleSubjectId, UserRole role = UserRole.User)
        {
            if (string.IsNullOrWhiteSpace(googleSubjectId))
            {
                throw new InvalidUserCredentialsException("Google Subject ID cannot be empty.");
            }

            var NewUser = new User(username, email, role, null, googleSubjectId);

            return NewUser;
        }


        //Add avatar image to user
        public void SetUserImage( string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new InvalidUserImageException("Incorrect image url");
            }

            ProfilePictureUrl = fileName;
        }
    }
}
