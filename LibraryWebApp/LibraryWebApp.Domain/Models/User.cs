using LibraryWebApp.Domain.Models;

namespace LibraryWebApp.Domain
{
    public class User
    {
        public User(string login, string password, 
            Role role, List<UserBook> takenBooks, List<RefreshToken> refreshTokens)
        {
            Login = login;
            Password = password;
            Role = role;
            TakenBooks = takenBooks;
            RefreshTokens = refreshTokens;
        }
        public string Login { get; }
        public string Password { get; }
        public Role Role { get; }
        public List<UserBook> TakenBooks { get; }
        public List<RefreshToken> RefreshTokens { get; }
    }
}
