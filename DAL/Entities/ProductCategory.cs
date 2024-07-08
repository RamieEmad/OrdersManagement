using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ProductCategory : BaseClass
    {
        public string categoryName { get; set; }
        public string categoryDesc { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
