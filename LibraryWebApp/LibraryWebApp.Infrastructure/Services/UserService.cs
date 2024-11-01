using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain;
using LibraryWebApp.Infrastructure.Exceptions;

namespace LibraryWebApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        public UserService(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public async Task Add(UserDTO dto)
        {
            var user = await _unitOfWork._userMapper.ToEntity(dto);
            await _unitOfWork.Users.Add(user);
        }

        public async Task<TokenDTO> Authorize(UserDTO dto)
        {
            var user = await _unitOfWork.Users.GetByLogin(dto.Login);
            if (user == null)
                throw new NotFoundException($"User with login {dto.Login} not found");            
            
            var access = _tokenService.GenerateAccessToken(user.Login, user.Role.ToString());
            var refresh = await _tokenService.GenerateRefreshToken(access, user.Role.ToString());

            return new TokenDTO
            {
                AccessToken = access,  
                RefreshToken = refresh.Token,
                UserLogin = user.Login,
            };
        }
       
        public async Task Delete(string login)
        {
            await _unitOfWork.Users.Delete(login);
        }

        public async Task<List<User>> GetAll()
        {
            return await _unitOfWork.Users.GetAll();
        }

        public async Task<User?> GetByLogin(string login)
        {
            return await _unitOfWork.Users.GetByLogin(login);
        }

        public async Task<TokenDTO> Register(UserDTO dto)
        {
            if (await _unitOfWork.Users.GetByLogin(dto.Login) != null)
                throw new Exception($"User with login {dto.Login} is already exists");           
            
            var user = await _unitOfWork._userMapper.ToEntity(dto);
          
            var accessToken= _tokenService.GenerateAccessToken(user.Login, user.Role.ToString());
            var refreshToken = await _tokenService.GenerateRefreshToken(user.Login, user.Role.ToString());

            user.RefreshTokens.Add(refreshToken);

            await _unitOfWork.Users.Add(user);
          
            return new TokenDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                UserLogin = dto.Login,
            };
        }

        public async Task Update(string login, UserDTO dto)
        {
            var user = await _unitOfWork._userMapper.ToEntity(dto);
            await _unitOfWork.Users.Update(login, user);
        }
        public async Task RegisterBookForUser(User user, Book book, DateTime receiptDate, DateTime returnDate)
        {
            await _unitOfWork.Users.RegisterBookForUser(user, book, receiptDate, returnDate);
        }
    }
}
