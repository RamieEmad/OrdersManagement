using DAL.Entities;

namespace PL.Models
{
    public class ProductViewModel 
    {
        public int Id { get; set; }
        public string prodName { get; set; }
        public string prodDesc { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public IEnumerable<ProductCategoryViewModel> ProductCategories { get; set; }




    }
}