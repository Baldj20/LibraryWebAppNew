using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases
{
    public interface IGetAuthorBooksUseCase
    {
        public Task<List<BookDTO>> GetBooks(Guid authorId);
    }
}
