using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.Abstractions.UseCases.UserUseCases
{
    public interface IGetUserByLoginUseCase
    {
        public Task<User?> GetByLogin(string login);
    }
}
