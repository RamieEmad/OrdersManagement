namespace DAL.Entities
{
    public class ProductCategory : BaseClass
    {
      
        public string categoryName  { get; set; }
        public string categoryDesc { get; set; }
        public IEnumerable<Product> Products { get; set; }


    }
}
