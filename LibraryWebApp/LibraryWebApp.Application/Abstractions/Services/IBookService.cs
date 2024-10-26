using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.Abstractions.Services
{
    public interface IBookService
    {
        public Task<List<BookDTO>> GetAll();
        public Task Add(BookDTO dto);
        public Task Update(string isbn, BookDTO dto);
        public Task Delete(string isbn);
        public Task<BookDTO> GetByISBN(string isbn);
    }
}
