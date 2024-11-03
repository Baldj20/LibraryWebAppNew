using LibraryWebApp.Application;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Application.Abstractions.UseCases.UserUseCases;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain;
using LibraryWebApp.Domain.Models;
using LibraryWebApp.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAddUserUseCase _addUserUseCase;
        private readonly IDeleteUserUseCase _deleteUserUseCase;
        private readonly IGetAllUsersUseCase _getAllUsersUseCase;
        private readonly IGetUserByLoginUseCase _getUserByLoginUseCase;
        private readonly IUpdateUserUseCase _updateUserUseCase;
        private readonly IGetPagedUsersUseCase _getPagedUsersUseCase;
        private readonly IRegisterUserUseCase _registerUserUseCase;
        private readonly IAuthorizeUseCase _authorizeUseCase;
        private readonly IRegisterBookForUserUseCase _registerBookForUserUseCase;
        private readonly ITokenService _tokenService;

        public UserController(IAddUserUseCase addUserUseCase, IDeleteUserUseCase deleteUserUseCase,
            IGetAllUsersUseCase getAllUsersUseCase, IGetUserByLoginUseCase getUserByLoginUseCase,
            IUpdateUserUseCase updateUserUseCase, IGetPagedUsersUseCase getPagedUsersUseCase,
            IRegisterUserUseCase registerUserUseCase, IAuthorizeUseCase authorizeUseCase,
            IRegisterBookForUserUseCase registerBookForUserUseCase, ITokenService tokenService)
        {
            _addUserUseCase = addUserUseCase;
            _deleteUserUseCase = deleteUserUseCase;
            _getAllUsersUseCase = getAllUsersUseCase;
            _getUserByLoginUseCase = getUserByLoginUseCase;
            _updateUserUseCase = updateUserUseCase;
            _getPagedUsersUseCase = getPagedUsersUseCase;
            _registerBookForUserUseCase = registerBookForUserUseCase;
            _authorizeUseCase = authorizeUseCase;
            _registerUserUseCase = registerUserUseCase;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserInfoDTO dto)
        {
            var tokens = await _registerUserUseCase.Register(dto);

            return Ok(new
            {
                accessToken = tokens.AccessToken,
                refreshToken = tokens.RefreshToken
            });
        }

        [HttpPost("authorize")]
        public async Task<ActionResult<TokenDTO>> Authorize([FromBody] UserInfoDTO dto)
        {
            var user = await _getUserByLoginUseCase.GetByLogin(dto.Login);

            if (user == null)
                throw new NotFoundException($"User with login {dto.Login} is not exist");


            if (user.Password != dto.Password)
                throw new InvalidPasswordException();

            var tokens = await _authorizeUseCase.Authorize(dto);

            return Ok(new TokenDTO
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
                UserLogin = user.Login,
            });
        }

        [HttpPost]
        public async Task<ActionResult> Add(UserDTO dto)
        {
            await _addUserUseCase.Add(dto);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{login}")]
        public async Task<ActionResult> Delete(string login)
        {
            await _deleteUserUseCase.Delete(login);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var users = await _getAllUsersUseCase.GetAll();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{login}")]
        public async Task<ActionResult<User>> GetByLogin(string login)
        {
            var user = await _getUserByLoginUseCase.GetByLogin(login);
            return Ok(user);
        }

        [Authorize]
        [HttpPut("{login}")]
        public async Task<ActionResult> Update(string login, UserDTO dto)
        {
            await _updateUserUseCase.Update(login, dto);
            return Ok();
        }

        [HttpGet("{pageNumber}, {pageSize}")]
        public async Task<ActionResult> GetPaged(int pageNumber, int pageSize)
        {
            var result = await _getPagedUsersUseCase.GetPagedUsers(new PaginationParams { PageNumber = pageNumber, PageSize = pageSize });
            return Ok(result);
        }
    }
}
