using BLL.Interfaces;
using DAL.Entities;
using DAL.OrderManagementDBContext;

namespace BLL.Repos
{
    public class ProductPriceHistoryRepo : GenericRepo<ProductPriceHistory>, IProductPriceHistoryRepo
    {
        public ProductPriceHistoryRepo(OrderManagementDBContext context) : base(context) { }
    }
}
