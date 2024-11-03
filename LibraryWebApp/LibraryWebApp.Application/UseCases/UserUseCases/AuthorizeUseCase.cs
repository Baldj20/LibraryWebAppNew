using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Application.Abstractions.UseCases.UserUseCases;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Infrastructure.Exceptions;

namespace LibraryWebApp.Application.UseCases.UserUseCases
{
    public class AuthorizeUseCase : IAuthorizeUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public AuthorizeUseCase(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public async Task<TokenDTO> Authorize(UserInfoDTO dto)
        {
            var user = await _unitOfWork.Users.GetByLogin(dto.Login);
            if (user == null)
                throw new NotFoundException($"User with login {dto.Login} not found");

            var access = _tokenService.GenerateAccessToken(user);
            var refresh = await _tokenService.GenerateRefreshToken(user.Login);

            return new TokenDTO
            {
                AccessToken = access,
                RefreshToken = refresh.Token,
                UserLogin = user.Login,
            };
        }
    }
}
