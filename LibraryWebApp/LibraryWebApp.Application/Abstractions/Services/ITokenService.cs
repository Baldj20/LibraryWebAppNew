using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain;
using LibraryWebApp.Domain.Models;

namespace LibraryWebApp.Application.Abstractions.Services
{
    public interface ITokenService : IService<TokenDTO>
    {
        public string GenerateAccessToken(User user);
        public Task<RefreshToken> GenerateRefreshToken(string userLogin);
        public Task<TokenDTO> RefreshJWTToken(TokenDTO oldToken);
    }
}
