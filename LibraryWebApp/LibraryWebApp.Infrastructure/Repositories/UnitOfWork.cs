using AutoMapper;
using LibraryWebApp.Application.Abstractions.Mappers;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Infrastructure.Mappers.Custom;

namespace LibraryWebApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;

        public IMapper _mapper { get; private set; }
        public IBookMapper _bookMapper { get; private set; }
        public IAuthorMapper _authorMapper { get; private set; }
        public ITokenMapper _tokenMapper { get; private set; }
        public IUserBookMapper _userbookMapper { get; private set; }
        public IUserMapper _userMapper { get; private set; }
        public IUserRepository Users { get; private set; }
        public IBookRepository Books { get; private set; }
        public IAuthorRepository Authors { get; private set; }
        public IUserBookRepository UserBooks { get; private set; }
        public IRefreshTokenRepository RefreshTokens { get; private set; }      

        public UnitOfWork(LibraryDbContext context, IMapper mapper)
        {
            _context = context;
            
            Users = new UserRepository(context, mapper);
            Books = new BookRepository(context, mapper);
            Authors = new AuthorRepository(context, mapper);
            UserBooks = new UserBookRepository(context, mapper);
            RefreshTokens = new RefreshTokenRepository(context, mapper);

            _mapper = mapper;
            _bookMapper = new BookMapper(Authors);
            _authorMapper = new AuthorMapper(Books, _bookMapper);
            _tokenMapper = new TokenMapper();
            _userbookMapper = new UserBookMapper(Users);
            _userMapper = new UserMapper(Users, _userbookMapper);
        }       

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
