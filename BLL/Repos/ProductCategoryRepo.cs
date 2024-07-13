using BLL.Interfaces;
using DAL.Entities;
using DAL.OrderManagementDBContext;

namespace BLL.Repos

{
    public class ProductCategoryRepo : IProductCategoryRepo
    {
        private readonly OrderManagementDBContext _context;
        public ProductCategoryRepo(OrderManagementDBContext context) 
        {
            _context = context;
        }
    }
}
