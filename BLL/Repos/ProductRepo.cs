﻿using BLL.Interfaces;
using DAL.Entities;
using DAL.OrderManagementDBContext;
using Microsoft.EntityFrameworkCore;

namespace BLL.Repos
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly OrderManagementDBContext _context;
        public ProductRepo(OrderManagementDBContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProductWithCategory()
        {

            var products = _context.Products.Include(p => p.ProductCategory).Include(p => p.ProductPriceHistories).ToList();
            return (products);
        }

        public Product GetById(int id)
        {
            var productById =  _context.Products.Find(id);
            if (productById == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return productById;
        }

        IQueryable<Product> IProductRepo.GetAllProductAndPricingHistory()
        {
            var products = _context.Products.Include(p => p.ProductCategory)
            .Include(p => p.ProductPriceHistories);

            return  (products);
        }



        public ProductPriceHistory GetActivePriceByProductId(int productId)
        {

            return _context.ProductPriceHistories.FirstOrDefault(pph => pph.ProductId == productId && pph.IsActive);

        }


    }
}

