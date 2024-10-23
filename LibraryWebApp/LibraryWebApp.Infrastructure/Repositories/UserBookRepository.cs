using AutoMapper;
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
        private readonly IUserRepository _userRepository;
        public UserBookRepository(LibraryDbContext context, IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _userRepository = userRepository;
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

            await Add(userBook);

            await _userRepository.Update(user.Id, user);
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
