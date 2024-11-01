using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain.Models;

namespace LibraryWebApp.Application.Abstractions.Mappers
{
    public interface ITokenMapper : ICustomMapper<TokenDTO, RefreshToken>
    {

    }
}
