using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.UseCases.AuthorUseCases
{
    public class GetAuthorByIdUseCase : IGetAuthorByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAuthorByIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<AuthorDTO> GetById(Guid id)
        {
            var author = await _unitOfWork.Authors.GetById(id);
            return _unitOfWork._authorMapper.ToDTO(author);
        }
    }
}
