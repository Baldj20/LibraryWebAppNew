using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.UserUseCases;

namespace LibraryWebApp.Application.UseCases.UserUseCases
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Delete(string login)
        {
            await _unitOfWork.Users.Delete(login);
        }
    }
}
