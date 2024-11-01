using AutoMapper;
using LibraryWebApp.Application.Abstractions.Mappers;

namespace LibraryWebApp.Application.Abstractions.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }
        IUserBookRepository UserBooks { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IMapper _mapper { get; }
        IBookMapper _bookMapper { get; }
        IAuthorMapper _authorMapper { get; }
        ITokenMapper _tokenMapper { get; }
        IUserBookMapper _userbookMapper { get; }
        IUserMapper _userMapper { get; }
        Task<int> CompleteAsync();
    }
}
