using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        Task AddAsync(global::PL.Models.ProductViewModel productViewModel);
    }
}
