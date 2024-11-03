using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases
{
    public interface IGetPagedAuthorsUseCase
    {
        public Task<List<AuthorDTO>> GetPagedAuthors(PaginationParams paginationParams);
    }
}
