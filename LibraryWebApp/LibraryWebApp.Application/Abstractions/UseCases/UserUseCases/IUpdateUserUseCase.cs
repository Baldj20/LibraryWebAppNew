using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.UserUseCases
{
    public interface IUpdateUserUseCase
    {
        public Task Update(string login, UserDTO dto);
    }
}
