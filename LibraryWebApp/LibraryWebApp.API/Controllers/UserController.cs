using LibraryWebApp.Application.Abstractions.Services;
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
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("registerInfo")]
        public async Task<ActionResult<RefreshToken>> Register([FromBody] UserDTO dto)
        {
            var tokens = await _userService.Register(dto);

            return Ok(new
            {
                accessToken = tokens.AccessToken,
                refreshToken = tokens.RefreshToken
            });
        }

        [HttpPost("authorizationInfo")]
        public async Task<ActionResult<TokenDTO>> Authorize([FromBody] UserDTO dto)
        {
            var user = await _userService.GetByLogin(dto.Login);

            if (user == null)
                throw new NotFoundException($"User with login {dto.Login} is not exist");


            if (user.Password != dto.Password)
                throw new InvalidPasswordException();

            var tokens = await _userService.Authorize(dto);

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
            await _userService.Add(dto);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{login}")]
        public async Task<ActionResult> Delete(string login)
        {
            await _userService.Delete(login);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            await _userService.GetAll();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{login}")]
        public async Task<ActionResult<User>> GetById(string login)
        {
            await _userService.GetByLogin(login);
            return Ok();
        }

        [Authorize]
        [HttpPut("{login}")]
        public async Task<ActionResult> Update(string login, UserDTO dto)
        {
            await _userService.Update(login, dto);
            return Ok();
        }
    }
}
