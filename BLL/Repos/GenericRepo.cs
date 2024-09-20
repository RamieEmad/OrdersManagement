using BLL.Interfaces;
using DAL.Entities;
using DAL.OrderManagementDBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BLL.Repos
{

    public class GenericRepo<T> : IGenericRepo<T> where T : BaseClass
    {
        #region CTOR
        private readonly OrderManagementDBContext _context;
        public GenericRepo(OrderManagementDBContext context)
        {
            _context = context;

        }
        #endregion

        #region CRUD
        //Adding
        public async Task AddAsync([FromForm] T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }


        //Deleting
        public async Task DeleteAsync(int id)
        {
            var entityToDelete = await _context.Set<T>().FindAsync(id);
            if (entityToDelete != null)
            {
                _context.Set<T>().Remove(entityToDelete);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<bool> DeleteArray(int productIds)
        {
            var entityToDelete = await _context.Set<T>().FindAsync(productIds);
            if (entityToDelete != null)
            {
                _context.Set<T>().Remove(entityToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        //Updating
        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        #endregion

        #region Get-Funs

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id)
        {
            var productById = await _context.Set<T>().FindAsync(id);
            if (productById == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return productById;
        }


        public async Task<IEnumerable<T>> GetAllActiveAsync() // Hold 4n
        {
            var isActiveProducts = await _context.Set<T>().Where(x => !x.IsDeleted && x.IsActive).ToListAsync();

            if (isActiveProducts == null)
                throw new Exception(nameof(isActiveProducts));
            else
                return isActiveProducts;
        }


        #endregion

        #region IS?
        public void ToggleActiveAsync(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                product.IsActive = !product.IsActive;
                _context.SaveChanges();
            }
        }


        //public void SelectAllProducts(bool selectAll)
        //{
        //    var products = _context.Products.ToList();
        //    foreach (var product in products)
        //    {
        //        product.IsSelected = selectAll;
        //    }
        //    _context.SaveChanges();
        //}
        #endregion

        #region 

        #endregion





    }
}
