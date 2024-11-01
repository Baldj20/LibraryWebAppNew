using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.Abstractions.Mappers
{
    public interface IUserMapper : ICustomMapper<UserDTO, User>
    {

    }
}
