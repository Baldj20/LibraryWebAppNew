using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain.Models;

namespace LibraryWebApp.Application.Abstractions.Mappers
{
    public interface IUserBookMapper : ICustomMapper<UserBookDTO, UserBook>
    {

    }
}
