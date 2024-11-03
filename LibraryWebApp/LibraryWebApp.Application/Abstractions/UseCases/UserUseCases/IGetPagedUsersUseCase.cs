using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.UserUseCases
{
    public interface IGetPagedUsersUseCase
    {
        public Task<List<UserDTO>> GetPagedUsers(PaginationParams paginationParams);
    }
}
