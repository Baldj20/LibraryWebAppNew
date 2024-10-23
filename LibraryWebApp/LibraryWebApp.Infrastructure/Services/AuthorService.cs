using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Domain;

namespace LibraryWebApp.Infrastructure.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task Add(Author entity)
        {
            await _authorRepository.Add(entity);
        }

        public async Task Delete(Guid id)
        {
            await _authorRepository.Delete(id);
        }

        public async Task<List<Author>> GetAll()
        {
            return await _authorRepository.GetAll();
        }

        public async Task<List<Book>> GetBooks(Guid authorId)
        {
            return await _authorRepository.GetBooks(authorId);
        }

        public async Task<Author> GetById(Guid id)
        {
            return await _authorRepository.GetById(id);
        }

        public async Task Update(Guid id, Author entity)
        {
            await _authorRepository.Update(id, entity);
        }
    }
}
