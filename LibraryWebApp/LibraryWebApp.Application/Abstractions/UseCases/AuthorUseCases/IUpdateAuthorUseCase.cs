using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases
{
    public interface IUpdateAuthorUseCase
    {
        public Task Update(Guid id, AuthorDTO dto);
    }
}
