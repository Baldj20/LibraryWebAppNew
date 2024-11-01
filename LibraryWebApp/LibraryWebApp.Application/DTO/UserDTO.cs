using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.DTO
{
    public class UserDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public List<UserBookDTO> TakenBooks { get; set; }
        public string RefreshToken { get; set; }
    }
}
