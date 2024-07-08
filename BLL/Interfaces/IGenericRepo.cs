

using Microsoft.AspNetCore.Mvc;

namespace BLL.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {

        Task AddAsync([FromBody]T entity);
        Task DeleteAsync(int id); 

        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllActiveAsync();
        Task<T> GetByIdAsync(int id);

        Task<T> UpdateAsync(int id, [FromBody]T entity);
          
        Task<bool> IsActive(int id);
        Task<bool> IsDeActive(int id);

        Task<bool> IsDeleted(int id);
    }
}
