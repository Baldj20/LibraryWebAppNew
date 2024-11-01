using LibraryWebApp.Application.Abstractions.Mappers;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain;
using LibraryWebApp.Domain.Models;

namespace LibraryWebApp.Infrastructure.Mappers.Custom
{
    public class UserMapper : IUserMapper
    {
        private readonly IUserBookMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserMapper(IUserRepository userRepository, IUserBookMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public UserDTO ToDTO(User entity)
        {
            var userDTO = new UserDTO
            {
                Login = entity.Login,
                Password = entity.Password,
                Role = entity.Role,
            };

            return userDTO;
        }

        public async Task<User> ToEntity(UserDTO dto)
        {
            var user = await _userRepository.GetByLogin(dto.Login);
            if (user != null) return user;

            var bookEntities = dto.TakenBooks;

            var takenBooks = new List<UserBook>();
            var refreshTokens = new List<RefreshToken>();           

            foreach (var bookEntity in bookEntities)
            {
                takenBooks.Add(await _mapper.ToEntity(bookEntity));
            }

            var newUser = new User(dto.Login, dto.Password, dto.Role, takenBooks, refreshTokens);

            return newUser;
        }
    }
}
