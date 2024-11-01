using LibraryWebApp.Application.Abstractions.Mappers;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain.Models;

namespace LibraryWebApp.Infrastructure.Mappers.Custom
{
    public class UserBookMapper : IUserBookMapper
    {
        private readonly IUserRepository _userRepository;
        public UserBookMapper(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserBookDTO ToDTO(UserBook entity)
        {
            return new UserBookDTO
            {
                ISBN = entity.ISBN,
                ReceiptDate = entity.ReceiptDate,
                ReturnDate = entity.ReturnDate,
                UserLogin = entity.User.Login
            };
        }

        public async Task<UserBook> ToEntity(UserBookDTO dto)
        {
            return new UserBook
            {
                ISBN = dto.ISBN,
                ReceiptDate = dto.ReceiptDate,
                ReturnDate = dto.ReturnDate,
                User = await _userRepository.GetByLogin(dto.UserLogin),
            };
        }
    }
}
