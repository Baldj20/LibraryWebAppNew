using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.UserUseCases;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.UseCases.UserUseCases
{
    public class AddUserUseCase : IAddUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddUserUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Add(UserDTO dto)
        {
            var user = await _unitOfWork._userMapper.ToEntity(dto);
            await _unitOfWork.Users.Add(user);
        }
    }
}
