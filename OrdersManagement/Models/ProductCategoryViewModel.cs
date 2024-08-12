﻿using DAL.Entities;
namespace PL.Models
{
    public class ProductCategoryViewModel 
    {
        public int Id { get; set; }
        public string categoryName { get; set; }
        public string categoryDesc { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
