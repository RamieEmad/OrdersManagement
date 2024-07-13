using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public class IProductCategoryRepo : IGenericRepo<ProductCategory>
    {
        public Task AddAsync([FromBody] ProductCategory entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductCategory>> GetAllActiveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategory> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsActive(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsDeActive(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsDeleted(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategory> UpdateAsync(int id, [FromBody] ProductCategory entity)
        {
            throw new NotImplementedException();
        }
        public Task<ProductCategory> Update(int id )
        { throw new NotImplementedException(); }

        public IEnumerable<ProductCategory> GetAll()
        {
            throw new NotImplementedException();
        }


    }
}
