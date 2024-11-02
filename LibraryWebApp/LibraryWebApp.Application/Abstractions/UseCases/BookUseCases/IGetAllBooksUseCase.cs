using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.BookUseCases
{
    public interface IGetAllBooksUseCase
    {
        public Task<List<BookDTO>> GetAll();
    }
}
