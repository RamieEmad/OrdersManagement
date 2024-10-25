

using Microsoft.AspNetCore.Http;

namespace DAL.Entities
{
    public class Product : BaseClass
    {
        
        public string prodName { get; set; }
        public string prodDesc { get; set; }
        public bool IsSelected { get; set; }
        
         
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        //NAR PROPS
        public virtual IEnumerable<ProductPriceHistory> ProductPriceHistories { get; set; }
        public virtual IEnumerable<UploadFile> UploadFiles { get; set; }


    }
}
