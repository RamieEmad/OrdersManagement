
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class ProductPriceHistory : BaseClass
    {
        [Required(ErrorMessage = " Please enter a value for Effective Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EffectiveDate { get; set; }
        
        [Required(ErrorMessage = "Please enter a price")]
        public double Price { get; set; }

        [Required(ErrorMessage = " Please enter a value for Discount")]
        public decimal Discount { get; set; }

        //[Required(ErrorMessage = " Please enter a value for Discount Expiry Date")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        //public DateTime ExpiryDiscountDate { get; set; }



        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
