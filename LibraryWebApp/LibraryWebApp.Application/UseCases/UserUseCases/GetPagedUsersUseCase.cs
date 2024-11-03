using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.UserUseCases;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.UseCases.UserUseCases
{
    public class GetPagedUsersUseCase : IGetPagedUsersUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPagedUsersUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<UserDTO>> GetPagedUsers(PaginationParams paginationParams)
        {
            var pagedUsers = await _unitOfWork.Users.GetPaged(paginationParams);

            var pagedUsersDTO = new List<UserDTO>();

            foreach (var user in pagedUsers.Items)
            {
                pagedUsersDTO.Add(_unitOfWork._userMapper.ToDTO(user));
            }

            return pagedUsersDTO;
        }
    }
}
