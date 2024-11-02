using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.UserUseCases
{
    public interface IAddUserUseCase
    {
        public Task Add(UserDTO dto);
    }
}
