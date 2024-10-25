using DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PL.Models
{
    public class ProductCategoryViewModel 
    {
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string categoryName { get; set; }

        [Display(Name = "Product Category")]
        public string categoryDesc { get; set; }
        [JsonIgnore]
        public Product Products { get; set; }
    }
}
