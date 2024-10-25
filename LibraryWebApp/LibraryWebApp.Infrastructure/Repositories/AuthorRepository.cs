using AutoMapper;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Domain;
using LibraryWebApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

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
                            .Select(av => new AuthorEntity
                            {
                                Id = av.Id,
                                Name = av.Name,
                                Surname = av.Surname,
                                BirthDate = av.BirthDate,
                                Country = av.Country,
                                Books = (List<BookEntity>)av.Books.Select(b => new BookEntity
                                {
                                    ISBN = b.ISBN,
                                    Title = b.Title,
                                    Genre = b.Genre,
                                    Description = b.Description,
                                    Author = new AuthorEntity() { Id = av.Id },
                                })
                            }).ToListAsync();
            //foreach (var authorEntity in authorEntities)
            //{
            //    await _context.Entry(authorEntity)
            //        .Collection(a => a.Books)
            //        .LoadAsync();
            //}
            //foreach (var authorEntity in authorEntities)
            //{
            //    authorEntity.Books = await _context.Books.Where(a => a.Author.Id == authorEntity.Id).ToListAsync();
            //}
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
                            .Select(av => new AuthorEntity
                            {
                                Id = av.Id,
                                Name = av.Name,
                                Surname = av.Surname,
                                BirthDate = av.BirthDate,
                                Country = av.Country,
                                Books = (List<BookEntity>)av.Books.Select(b => new BookEntity
                                {
                                    ISBN = b.ISBN,
                                    Title = b.Title,
                                    Genre = b.Genre,
                                    Description = b.Description,
                                    Author = new AuthorEntity() { Id = av.Id },
                                })
                            }).FirstOrDefaultAsync();

            return _mapper.Map<Author>(authorEntity);
        }

        public async Task Update(Guid id, Author entity)
        {
            //var books = entity.Books;
            //var bookEntities = _mapper.Map<List<BookEntity>>(books);

            //await _context.Authors.Where(author => author.Id == id).ExecuteUpdateAsync(x => x
            //.SetProperty(a => a.Name, a => entity.Name)
            //.SetProperty(a => a.Surname, a => entity.Surname)
            //.SetProperty(a => a.BirthDate, a => entity.BirthDate)
            //.SetProperty(a => a.Country, a => entity.Country));

            //var author = await _context.Authors.Where(n => n.Id == id)
            //                .Include(a => a.Books)
            //                .AsNoTracking()
            //                .Select(av => new AuthorEntity
            //                {
            //                    Id = av.Id,
            //                    Name = av.Name,
            //                    Surname = av.Surname,
            //                    BirthDate = av.BirthDate,
            //                    Country = av.Country,
            //                    Books = (List<BookEntity>)av.Books.Select(b => new BookEntity
            //                    {
            //                        ISBN = b.ISBN,
            //                        Title = b.Title,
            //                        Genre = b.Genre,
            //                        Description = b.Description,
            //                        Author = new AuthorEntity() { Id = av.Id },
            //                    })
            //                }).FirstOrDefaultAsync();

            //author.Books = bookEntities;

            //await _context.SaveChangesAsync();
            await Delete(id);
            await Add(entity);
        }
    }
}
