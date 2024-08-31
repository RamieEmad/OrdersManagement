using DAL.Entities;
using PagedList;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class ProductViewModel 
    {
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string prodName { get; set; }
        
        [Display(Name = "Product Description")]
        public string prodDesc { get; set; }

        public bool IsActive { get; set; }
        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }
        public bool IsSelected { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public IEnumerable<ProductCategoryViewModel> ProductCategories { get; set; }
       





    }
}