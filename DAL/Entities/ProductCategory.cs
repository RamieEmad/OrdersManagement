using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class ProductCategory : BaseClass
    {
        [Display(Name = "Product Name")]
        public string categoryName { get; set; }

        [Display(Name = "Product Category")]
        public string categoryDesc { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }


    }
}
