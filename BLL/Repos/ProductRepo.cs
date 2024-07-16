
using BLL.Interfaces;
using DAL.Entities;
using DAL.OrderManagementDBContext;

namespace BLL.Repos
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly OrderManagementDBContext _context;
        public ProductRepo(OrderManagementDBContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Set<Product>().ToList();
        }

    }

}
