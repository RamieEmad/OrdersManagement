using BLL.Interfaces;
using DAL.Entities;
using DAL.OrderManagementDBContext;
using Microsoft.EntityFrameworkCore;

namespace BLL.Repos
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly OrderManagementDBContext _context;
        public ProductRepo(OrderManagementDBContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProductWithCategory()
        {
            return _context.Products.Include(p => p.ProductCategory).ToList();
   
        }


        public Product GetById(int id)
        {
            var productById =  _context.Products.Find(id);
            if (productById == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return productById;
        }


    }
}

