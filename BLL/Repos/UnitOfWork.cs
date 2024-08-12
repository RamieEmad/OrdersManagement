using BLL.Interfaces;
using DAL.OrderManagementDBContext;
using System.Globalization;

namespace BLL.Repos
{    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderManagementDBContext _context;

        public IProductRepo ProductRepo { get; private set; }
        public IProductCategoryRepo ProductCategoryRepo { get; private set; }

        public UnitOfWork(OrderManagementDBContext context, IProductRepo productRepo,IProductCategoryRepo productCategoryRepo)

        //#CTOR
        {
            _context = context;
            ProductRepo = productRepo;
            ProductCategoryRepo = productCategoryRepo;
        }

        public void Save()
           => _context.SaveChanges();
        
    }
}
