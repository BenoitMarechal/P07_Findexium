namespace P7CreateRestApi.Repositories
{
    public interface IRepository<T>
    {
        //Task<T> GetById<U>(U id);

        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll() ;
        Task Add(T entity);
        Task Update(T entity);
        
        Task Delete(int id);
    }
}
