using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases
{
    public interface IGetAuthorByIdUseCase
    {
        public Task<AuthorDTO> GetById(Guid id);
    }
}
