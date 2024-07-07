using BLL.Interfaces;
using DAL.Entities;
using DAL.OrderManagementDBContext;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;
using System.Data.Entity;
using EntityState = System.Data.Entity.EntityState;

namespace BLL.Repos
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseClass
    {
        private readonly OrderManagementDBContext _context;
        public GenericRepo(OrderManagementDBContext context, IActionResultTypeMapper mapper)
        {
            _context = context;
            
        }


        public async Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var entityToDelete = await _context.Set<T>().FindAsync(id); // Create fntityToDelete to check availability
            if (entityToDelete != null)
            {
                _context.Set<T>().Remove(entityToDelete);
                await _context.SaveChangesAsync();
            }

        }


        public async Task <IEnumerable<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();


        public async Task<T> GetByIdAsync(int id)
        {
            var productById = await _context.Set<T>().FindAsync(id);
            if (productById == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return productById;
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            var updateProductById = await _context.Set<T>().FindAsync(id);

            if (updateProductById == null)
                throw new ArgumentNullException(nameof(entity));

            else
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                      return entity;
        }


        public async Task<IEnumerable<T>> GetAllActiveAsync()
        {
           var isActiveProducts = await _context.Set<T>().Where(x => !x.IsDeleted && x.IsActive).ToListAsync();

            if (isActiveProducts == null)
                throw new Exception(nameof(isActiveProducts));
            else
                return isActiveProducts;
        }


        public async Task IsDeleted(T entity)
        {
            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

    }
}
