using AutoMapper;
using LibraryWebApp.Application;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Domain;
using LibraryWebApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IMapper _mapper;
        private readonly LibraryDbContext _context;
        public AuthorRepository(LibraryDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task Add(Author entity)
        {
            var author = _mapper.Map<AuthorEntity>(entity);

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            await _context.Authors
                .Where(author => author.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<Author>> GetAll()
        {
            var authorEntities = await _context.Authors
                            .Include(a => a.Books)
                            .AsNoTracking()
                            .Select(ae => new AuthorEntity
                            {
                                Id = ae.Id,
                                Name = ae.Name,
                                Surname = ae.Surname,
                                BirthDate = ae.BirthDate,
                                Country = ae.Country,
                                Books = (List<BookEntity>)ae.Books.Select(b => new BookEntity
                                {
                                    ISBN = b.ISBN,
                                    Title = b.Title,
                                    Genre = b.Genre,
                                    Description = b.Description,
                                    Author = new AuthorEntity() { Id = ae.Id },
                                    Count = b.Count
                                })
                            }).ToListAsync();
            var authors = _mapper.Map<List<Author>>(authorEntities);

            return authors;
        }

        public async Task<List<Book>> GetBooks(Guid AuthorId)
        {
            var author = await GetById(AuthorId);

            return author.Books;
        }

        public async Task<Author> GetById(Guid id)
        {
            var authorEntity = await _context.Authors.Where(n => n.Id == id)
                            .Include(a => a.Books)
                            .AsNoTracking()
                            .Select(ae => new AuthorEntity
                            {
                                Id = ae.Id,
                                Name = ae.Name,
                                Surname = ae.Surname,
                                BirthDate = ae.BirthDate,
                                Country = ae.Country,
                                Books = (List<BookEntity>)ae.Books.Select(b => new BookEntity
                                {
                                    ISBN = b.ISBN,
                                    Title = b.Title,
                                    Genre = b.Genre,
                                    Description = b.Description,
                                    Author = new AuthorEntity() { Id = ae.Id },
                                    Count = b.Count
                                })
                            }).FirstOrDefaultAsync();

            return _mapper.Map<Author>(authorEntity);
        }

        public async Task<PagedResult<Author>> GetPaged(PaginationParams paginationParams)
        {
            var query = _context.Authors.AsQueryable();

            var totalItems = await query.CountAsync();

            var itemsQuery = query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize);

            var authorEntities = await itemsQuery
                            .Include(a => a.Books)
                            .AsNoTracking()
                            .Select(ae => new AuthorEntity
                            {
                                Id = ae.Id,
                                Name = ae.Name,
                                Surname = ae.Surname,
                                BirthDate = ae.BirthDate,
                                Country = ae.Country,
                                Books = (List<BookEntity>)ae.Books.Select(b => new BookEntity
                                {
                                    ISBN = b.ISBN,
                                    Title = b.Title,
                                    Genre = b.Genre,
                                    Description = b.Description,
                                    Author = new AuthorEntity() { Id = ae.Id },
                                    Count = b.Count
                                })
                            }).ToListAsync();
            
            var pagedAuthors = new PagedResult<Author>
            {
                Items = _mapper.Map<List<Author>>(authorEntities),
                TotalCount = totalItems,
                PageSize = paginationParams.PageSize,
                PageNumber = paginationParams.PageNumber,
                TotalPages = totalItems % paginationParams.PageSize == 0 ? 
                    totalItems % paginationParams.PageSize :
                    totalItems % paginationParams.PageSize + 1,
            };

            return pagedAuthors;
        }

        public async Task Update(Guid id, Author entity)
        {
            await Delete(id);
            await Add(entity);
        }
    }
}
