using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain.Models;

namespace LibraryWebApp.Application.Abstractions.Services
{
    public interface ITokenService : IService<TokenDTO>
    {
        public string GenerateAccessToken(string userLogin, string role);
        public Task<RefreshToken> GenerateRefreshToken(string userLogin, string role);
        public Task<TokenDTO> RefreshJWTToken(TokenDTO oldToken);
    }
}
