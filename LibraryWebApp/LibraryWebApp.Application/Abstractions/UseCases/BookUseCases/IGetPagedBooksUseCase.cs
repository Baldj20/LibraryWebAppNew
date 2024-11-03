using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.BookUseCases
{
    public interface IGetPagedBooksUseCase
    {
        public Task<List<BookDTO>> GetPagedBooks(PaginationParams paginationParams);
    }
}
