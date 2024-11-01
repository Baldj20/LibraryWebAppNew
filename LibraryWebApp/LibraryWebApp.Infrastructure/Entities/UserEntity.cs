namespace LibraryWebApp.Infrastructure.Entities
{
    public class UserEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public List<UserBookEntity> TakenBooks { get; set; }
        public List<RefreshTokenEntity> RefreshTokens { get; set; }
    }
}
