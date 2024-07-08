namespace DAL.Entities
{
    public class Product : BaseClass
    {

        public string prodName { get; set; }
        public string  prodDesc { get; set; }
        public ProductCategory productCategories { get; set; }
        
    }
}
