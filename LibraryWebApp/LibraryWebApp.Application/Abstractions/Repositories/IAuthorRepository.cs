using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.Abstractions.Repositories
{
    public interface IAuthorRepository : IRepository<Author>, IPagedRepository<Author>
    {
        public Task<List<Book>> GetBooks(Guid authorId);
    }
}
