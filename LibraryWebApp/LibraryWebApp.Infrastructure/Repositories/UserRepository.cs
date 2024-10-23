using AutoMapper;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Domain;
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

        public async Task Delete(Guid id)
        {
            await _context.Users
                .Where(user => user.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<User>> GetAll()
        {
            var userEntities = await _context.Authors
                .AsNoTracking()
                .ToListAsync();

            var users = _mapper.Map<List<User>>(userEntities);

            return users;
        }

        public async Task<User> GetById(Guid id)
        {
            var userEntity = await _context.Users.Where(user => user.Id == id).FirstAsync();

            return _mapper.Map<User>(userEntity);
        }

        public async Task Update(Guid id, User entity)
        {
            var books = entity.TakenBooks;
            var bookEntities = _mapper.Map<List<UserBookEntity>>(books);

            await _context.Users.Where(user => user.Id == id).ExecuteUpdateAsync(updUser => updUser
            .SetProperty(a => a.Login, a => entity.Login)
            .SetProperty(a => a.Password, a => entity.Password)
            .SetProperty(a => a.TakenBooks, a => bookEntities));
        }
    }
}
