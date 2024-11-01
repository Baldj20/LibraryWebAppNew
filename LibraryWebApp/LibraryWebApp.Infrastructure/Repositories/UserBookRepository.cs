using AutoMapper;
using LibraryWebApp.Application;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Domain;
using LibraryWebApp.Domain.Models;
using LibraryWebApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Infrastructure.Repositories
{
    public class UserBookRepository : IUserBookRepository
    {
        private readonly IMapper _mapper;
        private readonly LibraryDbContext _context;
        public UserBookRepository(LibraryDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task Add(UserBook entity)
        {
            var userBook = _mapper.Map<UserBookEntity>(entity);

            await _context.UserBooks.AddAsync(userBook);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string isbn)
        {
            await _context.UserBooks
                .Where(userBook => userBook.ISBN == isbn)
                .ExecuteDeleteAsync();
        }

        public async Task<List<UserBook>> GetAll()
        {
            var userBookEntities = await _context.UserBooks
                .AsNoTracking()
                .ToListAsync();

            var userBooks = _mapper.Map<List<UserBook>>(userBookEntities);

            return userBooks;
        }

        public async Task<UserBook> GetByISBN(string isbn)
        {
            var book = await _context.UserBooks.Where(userBook => userBook.ISBN == isbn).FirstAsync();

            return _mapper.Map<UserBook>(book);
        }

        public async Task<PagedResult<UserBook>> GetPaged(PaginationParams paginationParams)
        {
            var query = _context.UserBooks.AsQueryable();

            var totalItems = await query.CountAsync();

            var itemsQuery = query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize);

            var userBookEntities = await itemsQuery
                .AsNoTracking()
                .ToListAsync();

            var pagedUserBooks = new PagedResult<UserBook>
            {
                Items = _mapper.Map<List<UserBook>>(userBookEntities),
                TotalCount = totalItems,
                PageSize = paginationParams.PageSize,
                PageNumber = paginationParams.PageNumber,
                TotalPages = totalItems % paginationParams.PageSize == 0 ?
                    totalItems % paginationParams.PageSize :
                    totalItems % paginationParams.PageSize + 1,
            };

            return pagedUserBooks;
        }

        public async Task Update(string isbn, UserBook entity)
        {
            var user = entity.User;
            var userEntity = _mapper.Map<UserEntity>(user);

            await _context.UserBooks.Where(userBook => userBook.ISBN == isbn).ExecuteUpdateAsync(x => x
            .SetProperty(a => a.ReceiptDate, a => entity.ReceiptDate)
            .SetProperty(a => a.ReturnDate, a => entity.ReturnDate)
            .SetProperty(a => a.User, a => userEntity));
        }
    }
}
