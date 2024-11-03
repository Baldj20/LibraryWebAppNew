using AutoMapper;
using LibraryWebApp.Application;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Domain;
using LibraryWebApp.Domain.Models;
using LibraryWebApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly LibraryDbContext _context;
        public UserRepository(LibraryDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task Add(User entity)
        {
            var userEntity = _mapper.Map<UserEntity>(entity);

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string login)
        {
            await _context.Users
                .Where(user => user.Login == login)
                .ExecuteDeleteAsync();
        }

        public async Task<List<User>> GetAll()
        {
            var userEntities = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            var users = _mapper.Map<List<User>>(userEntities);

            return users;
        }

        public async Task<User?> GetByLogin(string login)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .Where(user => user.Login == login)
                .ToListAsync();

            if (userEntity.Count == 0) { return null; }

            return _mapper.Map<User>(userEntity.FirstOrDefault());
        }

        public async Task Update(string login, User entity)
        {
            var books = entity.TakenBooks;
            var bookEntities = _mapper.Map<List<UserBookEntity>>(books);

            await _context.Users.Where(user => user.Login == login).ExecuteUpdateAsync(updUser => updUser
            .SetProperty(a => a.Login, a => entity.Login)
            .SetProperty(a => a.Password, a => entity.Password)
            .SetProperty(a => a.TakenBooks, a => bookEntities));
        }
        public async Task RegisterBookForUser(User user, Book book,
            DateTime receiptDate, DateTime returnDate)
        {
            var userBook = new UserBook
            {
                ISBN = book.ISBN,
                ReceiptDate = receiptDate,
                ReturnDate = returnDate,
                User = user
            };

            user.TakenBooks.Add(userBook);

            var userBookEntity = _mapper.Map<UserBookEntity>(userBook);

            await _context.UserBooks.AddAsync(userBookEntity);
            await _context.SaveChangesAsync();

            await Update(user.Login, user);
        }

        public async Task<PagedResult<User>> GetPaged(PaginationParams paginationParams)
        {
            var query = _context.Users.AsQueryable();

            var totalItems = await query.CountAsync();

            var itemsQuery = query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize);

            var userEntities = await itemsQuery
                .AsNoTracking()
                .ToListAsync();

            var pagedUsers = new PagedResult<User>
            {
                Items = _mapper.Map<List<User>>(userEntities),
                TotalCount = totalItems,
                PageSize = paginationParams.PageSize,
                PageNumber = paginationParams.PageNumber,
                TotalPages = totalItems % paginationParams.PageSize == 0 ?
                    totalItems % paginationParams.PageSize :
                    totalItems % paginationParams.PageSize + 1,
            };

            return pagedUsers;
        }
    }
}
