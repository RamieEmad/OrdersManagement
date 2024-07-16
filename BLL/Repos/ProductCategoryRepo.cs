using BLL.Interfaces;
using DAL.Entities;
using DAL.OrderManagementDBContext;

namespace BLL.Repos

{
    
    public class ProductCategoryRepo : GenericRepo<ProductCategory> ,IProductCategoryRepo
    {
        private readonly OrderManagementDBContext _context;
        public ProductCategoryRepo(OrderManagementDBContext context) : base(context)
        {
            _context = context;
        }

    }
}
