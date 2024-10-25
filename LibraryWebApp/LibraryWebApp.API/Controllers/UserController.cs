using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Domain;
using LibraryWebApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> Add(User entity)
        {
            await _userService.Add(entity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _userService.Delete(id);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            await _userService.GetAll();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(Guid userId)
        {
            await _userService.GetById(userId);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, User entity)
        {
            await _userService.Update(id, entity);
            return Ok();
        }
    }
}
