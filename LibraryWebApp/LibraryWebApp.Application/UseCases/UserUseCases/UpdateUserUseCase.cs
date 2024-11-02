using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.UserUseCases;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.UseCases.UserUseCases
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Update(string login, UserDTO dto)
        {
            var user = await _unitOfWork._userMapper.ToEntity(dto);
            await _unitOfWork.Users.Update(login, user);
        }
    }
}
