namespace DAL.Entities
{
    public class ShoppingCart : BaseClass
    {

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
