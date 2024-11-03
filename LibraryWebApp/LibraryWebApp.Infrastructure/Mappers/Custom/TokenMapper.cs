using LibraryWebApp.Application.Abstractions.Mappers;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain.Models;

namespace LibraryWebApp.Infrastructure.Mappers.Custom
{
    public class TokenMapper : ITokenMapper
    {
        public TokenDTO ToDTO(RefreshToken entity)
        {
            return new TokenDTO
            {
                AccessToken = string.Empty,
                RefreshToken = entity.Token,
                UserLogin = entity.UserLogin,
            };
        }

        public async Task<RefreshToken> ToEntity(TokenDTO dto)
        {
            return new RefreshToken(Guid.NewGuid(), dto.RefreshToken,
                DateTime.UtcNow.AddDays(30), false, DateTime.UtcNow, 
                dto.UserLogin);
        }
    }
}
