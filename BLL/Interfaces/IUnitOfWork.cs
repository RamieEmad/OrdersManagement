namespace BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepo ProductRepo { get; }
        public IProductCategoryRepo ProductCategoryRepo { get; }
        void Save();
    }
}
