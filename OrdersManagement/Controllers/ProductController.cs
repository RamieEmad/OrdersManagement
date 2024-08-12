using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using PL.Models;

namespace PL.Controllers
{
    public class ProductController : Controller
    {
        #region DI
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;
        private readonly IGenericRepo<ProductCategory> _generiCategory;
        private readonly IGenericRepo<Product> _genericProduct;

        public ProductController
            (IUnitOfWork unitOfWork,
            IMapper mapper,
            IGenericRepo<ProductCategory> genericRepo,
            ILogger<ProductController> logger,
            IGenericRepo<Product> productRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _generiCategory = genericRepo;
            _logger = logger;
            _genericProduct = productRepo; 
        }

        #endregion

        #region CUD

        #region Create
        [HttpGet]
        public async Task<ActionResult> Add()
        {
            var categories = await _generiCategory.GetAllAsync();
            
            if (categories == null)
            {
                return NotFound();
            }
            
           // Get the Categories Seperated from => Product.Categories
            var productViewModel = new ProductViewModel
            {
                ProductCategories = categories.Select(x => new ProductCategoryViewModel
                {
                    Id = x.Id,
                    categoryName = x.categoryName,
                }
            )
            };

            return View(productViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(ProductViewModel productViewModel)
        {
            if (productViewModel == null)
            {
                return NotFound();
            }

            var product = new Product
            {
                prodName = productViewModel.prodName,
                prodDesc = productViewModel.prodDesc,
                ProductCategoryId = productViewModel.ProductCategoryId,
                IsActive = productViewModel.IsActive,
                IsDeleted = productViewModel.IsDeleted
            };

                var productToAdd = _mapper.Map<Product>(product);
                await _genericProduct.AddAsync(productToAdd);

                return RedirectToAction(nameof(List));
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var getByIdProduct = await _genericProduct.GetByIdAsync(id);
            var productViewModel = _mapper.Map<ProductViewModel>(getByIdProduct);
            return View(productViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, [FromForm] ProductViewModel productViewModel)
        {
            if (id == productViewModel.Id)
            {
                var productDeleteconfirmed = await _genericProduct.GetByIdAsync(id);
                var viewModel = _mapper.Map<Product>(productDeleteconfirmed);

                // Already checked " if not null "
                await _genericProduct.DeleteAsync(viewModel.Id);
                
            }
            return RedirectToAction(nameof(List));
        }

        #endregion

        #region Update
        //Get/ProductById/ToUpdate

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var getByIdProduct = await _genericProduct.GetByIdAsync(id);

            if (getByIdProduct == null)
            {
                return NotFound();
            }

            var getProductByIdViewModel = _mapper.Map<ProductViewModel>(getByIdProduct);
            return View(getProductByIdViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, [FromForm] ProductViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest("Invalid product id");
            }

            var product = await _genericProduct.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            var updatedProduct = _mapper.Map(viewModel, product);
            await _genericProduct.UpdateAsync(updatedProduct);

            return RedirectToAction("List");
        }

        #endregion

        #region Details

        public async Task<IActionResult> Details(int id)
        {
            var getByIdProduct = await _genericProduct.GetByIdAsync(id);
            var productViewModel = _mapper.Map<ProductViewModel>(getByIdProduct);
            return View(productViewModel);
        }
        #endregion

        #region Read-GET
        [HttpGet]
        public ActionResult List()
        {
            var getAllProduct = _unitOfWork.ProductRepo.GetAllProductWithCategory();
            var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(getAllProduct);

            if (productViewModels == null)
            {
                return NotFound();
            }

            return View(productViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var getByIdProduct = await _genericProduct.GetByIdAsync(id);
            var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(getByIdProduct);

            if (productViewModels == null)
            {
                return NotFound();
            }

            return View();
        }


        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAllIsActive() // Hold 4n
        {
            var isActiveProducts = await _genericProduct.GetAllActiveAsync();
            var productViewModel = _mapper.Map<ProductViewModel>(isActiveProducts);

            if (productViewModel == null)
            {
                return NotFound();
            }
            return View();

        }
        #endregion

        #region IS?
        [HttpPost]
        public IActionResult ToggleIsActive(int id)
        {
            _genericProduct.ToggleActiveAsync(id);
            return RedirectToAction("List");
        }


        public async Task<bool> IsActive(int id)
        {
            bool isActiveProduct = await _genericProduct.IsActive(id);
            return isActiveProduct;
        }


        public async Task<bool> IsDeActive(int id)
        {
            bool isDeActiveProduct = await _genericProduct.IsDeActive(id);
            
            return isDeActiveProduct;
        }


        public async Task<bool> IsDelete(int id)
        {
            var isDeletedProduct = await _genericProduct.IsDeleted(id);
            return isDeletedProduct;
            
        }

        #endregion


        #endregion
    }
}
