using PL.Models;

public class ShoppingCartViewModel
{
    public int Id { get; set; } 
    public List<ProductViewModel> CartItems { get; set; }
}