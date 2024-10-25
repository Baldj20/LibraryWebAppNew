namespace LibraryWebApp.Application.Abstractions.Services
{
    public interface IService<T>
    {
        public Task<List<T>> GetAll();
        public Task Add(T dto);
        public Task Update(Guid id, T dto);
        public Task Delete(Guid id);
        public Task<T> GetById(Guid id);
    }
}
