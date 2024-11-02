namespace LibraryWebApp.Application.Abstractions.UseCases.BookUseCases
{
    public interface IDeleteBookUseCase
    {
        public Task Delete(string isbn);
    }
}
