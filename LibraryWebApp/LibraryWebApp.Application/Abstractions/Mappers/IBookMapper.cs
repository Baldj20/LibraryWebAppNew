using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.Abstractions.Mappers
{
    public interface IBookMapper : ICustomMapper<BookDTO, Book>
    {

    }
}
