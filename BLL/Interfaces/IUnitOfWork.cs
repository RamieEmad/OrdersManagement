namespace BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepo ProductRepo { get; }
        public IProductCategoryRepo ProductCategoryRepo { get; }
        public IUploadFileRepo UploadFileRepo { get; }
        void Save();
    }
}
