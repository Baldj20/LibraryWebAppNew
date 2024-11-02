namespace LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases
{
    public interface IDeleteAuthorUseCase
    {
        public Task Delete(Guid id);
    }
}
