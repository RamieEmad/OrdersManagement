using BLL.Interfaces;
using DAL.Entities;
using DAL.OrderManagementDBContext;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace BLL.Repos
{
    
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseClass
    {
        private readonly OrderManagementDBContext _context;
        public GenericRepo(OrderManagementDBContext context)
        {
            _context = context;
        }


        public  Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChangesAsync();
        }


        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChangesAsync();
        }


        public async Task <IEnumerable<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();
        

        public async Task<T> GetByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);


        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified; 
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<IEnumerable<T>> GetAllActiveAsync()
            => await _context.Set<T>().Where(x => !x.IsDeleted && x.IsActive).ToListAsync();
        


        public async Task SoftDeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }


    }
}
