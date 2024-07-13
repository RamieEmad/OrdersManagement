namespace PL.Models
{
    public class ProductViewModel
    {
        
        public string prodName { get; set; }
        public string prodDesc { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int SelectedCategoryId { get; set; }
        public IEnumerable<ProductCategoryViewModel> ProductCategoryViewModel { get; set; }

    }
}
