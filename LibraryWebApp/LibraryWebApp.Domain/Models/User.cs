using LibraryWebApp.Domain.Models;

namespace LibraryWebApp.Domain
{
    public class User
    {
        public User(Guid id, string login, string password, 
            Role role, List<UserBook> takenBooks)
        {
            Id = id;
            Login = login;
            Password = password;
            Role = role;
            TakenBooks = takenBooks;
        }
        public Guid Id { get; set; }
        public string Login {  get; }
        public string Password { get; }
        public Role Role { get; }
        public List<UserBook> TakenBooks { get; }
    }
}
