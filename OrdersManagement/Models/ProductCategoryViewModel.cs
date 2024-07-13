using DAL.Entities;

namespace PL.Models
{
    public class ProductCategoryViewModel 
    {
        public int id { get; set; } 
        public string categoryName { get; set; }
        public string categoryDesc { get; set; }
        public IEnumerable<ProductViewModel> ProductsViewModel{ get; set;}
    }
}
