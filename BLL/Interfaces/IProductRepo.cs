using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        IEnumerable<Product> GetAllProductWithCategory();
        public Product GetById(int id);
        public IQueryable<Product> GetAllProductAndPricingHistory();
        public ProductPriceHistory GetActivePriceByProductId(int productId);


    }
}
