

namespace BLL.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {

        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task <IEnumerable<T>> GetAllActiveAsync();
        Task SoftDeleteAsync(int id);



    }
}
