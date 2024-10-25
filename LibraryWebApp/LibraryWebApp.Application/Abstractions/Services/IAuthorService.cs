using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.Services
{
    public interface IAuthorService : IService<AuthorDTO>
    {
        public Task<List<BookDTO>> GetBooks(Guid authorId);
    }
}
