using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.Abstractions.Repositories
{
    public interface IRepository<T>
    {
        public Task<List<T>> GetAll();       
        public Task Add(T entity);
        public Task Update(Guid id, T entity);
        public Task Delete(Guid id);
        public Task<T> GetById(Guid id);
    }
}
