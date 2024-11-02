using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.Abstractions.UseCases.UserUseCases
{
    public interface IGetAllUsersUseCase
    {
        public Task<List<User>> GetAll();
    }
}
