

namespace BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepo ProductRepo { get; } void Save();

    }
}
