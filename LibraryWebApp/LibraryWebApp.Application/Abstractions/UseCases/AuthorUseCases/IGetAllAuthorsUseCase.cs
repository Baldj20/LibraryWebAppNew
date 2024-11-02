using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases
{
    public interface IGetAllAuthorsUseCase
    {
        public Task<List<AuthorDTO>> GetAll();
    }
}
