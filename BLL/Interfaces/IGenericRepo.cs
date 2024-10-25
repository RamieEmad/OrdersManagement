﻿namespace BLL.Interfaces
{

    public interface IGenericRepo<T> where T : class
    {
        Task AddAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> DeleteArray(int productIds);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllActiveAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> UpdateAsync(T entity);
        void ToggleActiveAsync(int id);
        
        



    }
}
