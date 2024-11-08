﻿namespace LibraryWebApp.Infrastructure.Entities
{
    public class RefreshTokenEntity
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string UserLogin { get; set; }
        public UserEntity User { get; set; }
    }
}
