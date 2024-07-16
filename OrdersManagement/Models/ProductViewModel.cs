using DAL.Entities;

namespace PL.Models
{
    public class ProductViewModel : BaseClass
    {
        
        public string prodName { get; set; }
        public string prodDesc { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public int ProductCategoryId { get; set; }
        public IEnumerable<ProductCategoryViewModel> ProductCategories { get; set; }


    }
}