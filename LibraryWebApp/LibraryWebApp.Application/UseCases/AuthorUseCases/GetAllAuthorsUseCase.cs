using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.UseCases.AuthorUseCases
{
    public class GetAllAuthorsUseCase : IGetAllAuthorsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllAuthorsUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<AuthorDTO>> GetAll()
        {
            var authorDTOList = new List<AuthorDTO>();

            var authors = await _unitOfWork.Authors.GetAll();

            foreach (var author in authors)
            {
                authorDTOList.Add(_unitOfWork._authorMapper.ToDTO(author));
            }

            return authorDTOList;
        }
    }
}
