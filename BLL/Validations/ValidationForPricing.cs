//using DAL.Entities;
//using System.ComponentModel.DataAnnotations;

//namespace BLL.Validations
//{
//    public class ValidationForPricing : ValidationAttribute
//    {
//        var productPriceHistory = (ProductPriceHistory)value;

//        var existingActivePrice = _context.ProductPriceHistories
//            .Where(pph => pph.ProductId == productPriceHistory.ProductId && pph.IsActive)
//            .FirstOrDefault();

//        return existingActivePrice == null;
//    }
//}
