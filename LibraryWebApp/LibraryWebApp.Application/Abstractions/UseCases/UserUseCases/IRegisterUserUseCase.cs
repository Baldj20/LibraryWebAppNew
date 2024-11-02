using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.UserUseCases
{
    public interface IRegisterUserUseCase
    {
        public Task<TokenDTO> Register(UserDTO dto);
    }
}
