using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.UserUseCases
{
    public interface IAuthorizeUseCase
    {
        public Task<TokenDTO> Authorize(UserDTO dto);
    }
}
