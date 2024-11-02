namespace LibraryWebApp.Application.Abstractions.UseCases.UserUseCases
{
    public interface IDeleteUserUseCase
    {
        public Task Delete(string login);
    }
}
