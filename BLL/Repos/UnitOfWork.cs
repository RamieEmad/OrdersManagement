using BLL.Interfaces;
using DAL.OrderManagementDBContext;

namespace BLL.Repos
{    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderManagementDBContext _context;
        public IProductRepo ProductRepo { get; private set; }
        

        public UnitOfWork(OrderManagementDBContext context, IProductRepo productRepo)
        {
            _context = context;
            ProductRepo = productRepo;
        }

        public void Save()
           => _context.SaveChanges();
        
    }
}
