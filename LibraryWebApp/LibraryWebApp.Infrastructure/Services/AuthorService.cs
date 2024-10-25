using LibraryWebApp.Application.Abstractions.Mappers;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Infrastructure.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorMapper _authorMapper;
        private readonly IBookMapper _bookMapper;
        public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository, 
            IAuthorMapper authorMapper, IBookMapper bookMapper)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _authorMapper = authorMapper;
            _bookMapper = bookMapper;
        }
        public async Task Add(AuthorDTO dto)
        {
            var author = await _authorMapper.ToEntity(dto);

            await _authorRepository.Add(author);
        }

        public async Task Delete(Guid id)
        {
            await _authorRepository.Delete(id);
        }

        public async Task<List<AuthorDTO>> GetAll()
        {
            var authorDTOList = new List<AuthorDTO>();

            var authors = await _authorRepository.GetAll();

            foreach (var author in authors)
            {
                authorDTOList.Add(_authorMapper.ToDTO(author));
            }

            return authorDTOList;
        }

        public async Task<List<BookDTO>> GetBooks(Guid authorId)
        {
            var bookList = await _authorRepository.GetBooks(authorId);

            var bookDTOList = new List<BookDTO>();

            foreach (var book in bookList)
            {
                bookDTOList.Add(_bookMapper.ToDTO(book));
            }

            return bookDTOList;
        }

        public async Task<AuthorDTO> GetById(Guid id)
        {
            var author = await _authorRepository.GetById(id);
            return _authorMapper.ToDTO(author);
        }

        public async Task Update(Guid id, AuthorDTO dto)
        {
            var author = await _authorMapper.ToEntity(dto);

            await _authorRepository.Update(id, author);
        }
    }
}
