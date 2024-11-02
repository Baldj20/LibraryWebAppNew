using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Application.Abstractions.UseCases.UserUseCases;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.UseCases.UserUseCases
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        public RegisterUserUseCase(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public async Task<TokenDTO> Register(UserDTO dto)
        {
            if (await _unitOfWork.Users.GetByLogin(dto.Login) != null)
                throw new Exception($"User with login {dto.Login} is already exists");

            var user = await _unitOfWork._userMapper.ToEntity(dto);

            var accessToken = _tokenService.GenerateAccessToken(user.Login, user.Role.ToString());
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
    }
}
