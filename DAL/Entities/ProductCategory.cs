using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class ProductCategory : BaseClass
    {
        [Display(Name = "Product Name")]
        public string categoryName  { get; set; }
        [Display(Name = "Product Category")]
        public string categoryDesc { get; set; }
        public IEnumerable<Product> Products { get; set; }


    }
}
