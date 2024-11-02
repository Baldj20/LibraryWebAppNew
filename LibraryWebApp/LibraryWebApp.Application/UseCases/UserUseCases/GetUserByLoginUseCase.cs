using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.UserUseCases;
using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.UseCases.UserUseCases
{
    public class GetUserByLoginUseCase : IGetUserByLoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUserByLoginUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<User?> GetByLogin(string login)
        {
            return await _unitOfWork.Users.GetByLogin(login);
        }
    }
}
