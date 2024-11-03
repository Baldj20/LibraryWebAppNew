namespace LibraryWebApp.Domain.Models
{
    public class RefreshToken
    {
        public RefreshToken(Guid id, string token, 
            DateTime expires, bool isRevoked, DateTime created, string userLogin)
        {
            Id = id;
            Token = token;
            Expires = expires;
            IsRevoked = isRevoked;
            Created = created;
            UserLogin = userLogin;
        }
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string UserLogin { get; set; }
    }
}
