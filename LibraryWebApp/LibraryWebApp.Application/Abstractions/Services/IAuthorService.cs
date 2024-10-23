using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.Abstractions.Services
{
    public interface IAuthorService : IService<Author>
    {
        public Task<List<Book>> GetBooks(Guid authorId);
    }
}
