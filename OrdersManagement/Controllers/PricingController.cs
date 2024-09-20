using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using PL.Models;


namespace PL.Controllers
{
    public class PricingController : Controller
    {
        #region DI
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;
        private readonly IGenericRepo<ProductCategory> _generiCategory;
        private readonly IGenericRepo<Product> _genericProduct;
        private readonly IGenericRepo<ProductPriceHistory> _genericPriceHistory;


        public PricingController(
            ILogger<ProductController> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IGenericRepo<Product> productRepo,
            IGenericRepo<ProductCategory> genericRepo,
            IGenericRepo<ProductPriceHistory> productPriceRepo)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _genericProduct = productRepo;
            _generiCategory = genericRepo;
            _genericPriceHistory = productPriceRepo;

        }
        #endregion

        #region CRUD

        #region AddPricing
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var allProducts = await _genericProduct.GetAllAsync();
            if (allProducts == null)
            {
                return NotFound();
            }

            var productPriceHistoryviewModel = new ProductPriceHistoryViewModel
            {
                Products = allProducts.Select(x =>
                new ProductViewModel
                {
                    Id = x.Id,
                    prodName = x.prodName,
                }
            )
            };

            return View(productPriceHistoryviewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductPriceHistoryViewModel viewModel)
        {
            var getActivePrice = _unitOfWork.ProductRepo.GetActivePriceByProductId(viewModel.ProductId);
            if (getActivePrice != null)
            {
                getActivePrice.IsActive = false;
                await _genericPriceHistory.UpdateAsync(getActivePrice);
            }
            viewModel.IsActive = true;

            var productPriceHistory = new ProductPriceHistory
            {
                Id = viewModel.Id,
                Price = viewModel.Price,
                EffectiveDate = viewModel.EffectiveDate,
                Discount = viewModel.Discount,
                ProductId = viewModel.ProductId,
                IsActive = viewModel.IsActive
            };

            var productToAdd = _mapper.Map<ProductPriceHistory>(productPriceHistory);
            await _genericPriceHistory.AddAsync(productToAdd);
            return RedirectToAction("Add");

        }
        #endregion

        #endregion

    }
}

