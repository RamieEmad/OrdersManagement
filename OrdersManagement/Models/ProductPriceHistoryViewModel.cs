using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class ProductPriceHistoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = " Please enter a value for Effective Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EffectiveDate { get; set; }

        [Required(ErrorMessage = "Please enter a price")]
        public double Price { get; set; }

        [Required(ErrorMessage = " Please enter a value for Discount")]
        public decimal Discount { get; set; }

        public bool IsActive {  get; set; }
        
        public bool IsDeleted {  get; set; }
        
        public int ProductId { get; set; }
        
        public IEnumerable<ProductViewModel> Products { get; set; }

    }
}
