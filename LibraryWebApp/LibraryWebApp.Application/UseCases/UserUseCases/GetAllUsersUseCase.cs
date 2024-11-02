using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.UserUseCases;
using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.UseCases.UserUseCases
{
    public class GetAllUsersUseCase : IGetAllUsersUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<User>> GetAll()
        {
            return await _unitOfWork.Users.GetAll();
        }
    }
}
