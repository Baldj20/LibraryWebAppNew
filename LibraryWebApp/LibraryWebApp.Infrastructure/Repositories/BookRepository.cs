using AutoMapper;
using LibraryWebApp.Application;
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
            var author = _context.Authors.Where(author => author.Id == entity.Author.Id)
                .Include(author => author.Books).FirstOrDefault();
            if (author == null) throw new NotFoundException("Author not found");
            book.Author = author;
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
            var bookEntities = await _context.Books.Include(b => b.Author)
                .Select(book => new BookEntity
                {
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Genre = book.Genre,
                    Description = book.Description,
                    Count = book.Count,
                    Author = new AuthorEntity()
                    {
                        Id = book.Author.Id,
                    }
                })
                .AsNoTracking()
                .ToListAsync();

            var books = _mapper.Map<List<Book>>(bookEntities);

            return books;
        }

        public async Task<Book> GetByISBN(string isbn)
        {
            try
            {
                var book = await _context.Books.Where(book => book.ISBN == isbn)
                    .Include(book => book.Author)
                    .Select(book => new BookEntity
                    {
                        ISBN = book.ISBN,
                        Title = book.Title,
                        Genre = book.Genre,
                        Description = book.Description,
                        Count = book.Count,
                        Author = new AuthorEntity()
                        {
                            Id = book.Author.Id,
                        }
                    }).FirstAsync();

                return _mapper.Map<Book>(book);
            }
            catch (Exception ex)
            {
                throw new NotFoundException($"Book with ISBN {isbn} not found");
            }            
        }

        public async Task<PagedResult<Book>> GetPaged(PaginationParams paginationParams)
        {
            var query = _context.Books.AsQueryable();

            var totalItems = await query.CountAsync();

            var itemsQuery = query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize);

            var bookEntities = await _context.Books
                .Include(b => b.Author)
                .Select(book => new BookEntity
                {
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Genre = book.Genre,
                    Description = book.Description,
                    Count = book.Count,
                    Author = new AuthorEntity()
                    {
                        Id = book.Author.Id,
                    }
                })
                .AsNoTracking()
                .ToListAsync();

            var pagedBooks = new PagedResult<Book>
            {
                Items = _mapper.Map<List<Book>>(bookEntities),
                TotalCount = totalItems,
                PageSize = paginationParams.PageSize,
                PageNumber = paginationParams.PageNumber,
                TotalPages = totalItems % paginationParams.PageSize == 0 ?
                    totalItems % paginationParams.PageSize :
                    totalItems % paginationParams.PageSize + 1,
            };

            return pagedBooks;
        }

        public async Task Update(string isbn, Book entity)
        {
            await Delete(isbn);
            await Add(entity);
        }
    }
}
