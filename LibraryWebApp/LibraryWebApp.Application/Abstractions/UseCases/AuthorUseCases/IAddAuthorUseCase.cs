using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases
{
    public interface IAddAuthorUseCase
    {
        public Task Add(AuthorDTO dto);
    }
}
