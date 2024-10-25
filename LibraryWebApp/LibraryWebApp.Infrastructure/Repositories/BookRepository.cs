using AutoMapper;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Domain;
using LibraryWebApp.Infrastructure.Entities;
using LibraryWebApp.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IMapper _mapper;
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task Add(Book entity)
        {
            var book = _mapper.Map<BookEntity>(entity);

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string isbn)
        {
            await _context.Books
                .Where(book => book.ISBN == isbn)
                .ExecuteDeleteAsync();
        }

        public async Task<List<Book>> GetAll()
        {
            var bookEntities = await _context.Books
                .AsNoTracking()
                .ToListAsync();

            var books = _mapper.Map<List<Book>>(bookEntities);

            return books;
        }

        public async Task<Book> GetByISBN(string isbn)
        {
            var book = await _context.Books.Where(book => book.ISBN == isbn).FirstAsync();
            if (book == null) throw new NotFoundException(isbn);
            return _mapper.Map<Book>(book);
        }

        public async Task Update(string isbn, Book entity)
        {
            var authorEntity = _mapper.Map<AuthorEntity>(entity);

            await _context.Books.Where(book => book.ISBN == isbn).ExecuteUpdateAsync(book => book
            .SetProperty(a => a.Title, a => entity.Title)
            .SetProperty(a => a.Genre, a => entity.Genre)
            .SetProperty(a => a.Description, a => entity.Description)
            .SetProperty(a => a.Author, a => authorEntity));
        }
    }
}
