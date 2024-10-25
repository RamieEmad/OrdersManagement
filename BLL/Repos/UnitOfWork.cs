using BLL.Interfaces;
using DAL.OrderManagementDBContext;

namespace BLL.Repos
{    
    public class UnitOfWork : IUnitOfWork
    {
        #region DI

        private readonly OrderManagementDBContext _context;
        public IProductRepo ProductRepo { get; private set; }
        public IProductCategoryRepo ProductCategoryRepo { get; private set; }
        public IUploadFileRepo UploadFileRepo { get; private set; }
        public ICartRepo CartRepo { get; private set; }



        public UnitOfWork
            (
            OrderManagementDBContext context,
            IProductRepo productRepo,
            IProductCategoryRepo productCategoryRepo,
            UploadFileRepo uploadFileRepo
            )

        //CTOR
        {
            _context = context;
            ProductRepo = productRepo;
            ProductCategoryRepo = productCategoryRepo;
            UploadFileRepo = uploadFileRepo;
        }

        #endregion

        public void Save()
           => _context.SaveChanges();

        void IUnitOfWork.Save()
        {
            throw new NotImplementedException();
        }
    }
}
