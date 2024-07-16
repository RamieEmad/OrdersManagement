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
        private readonly IGenericRepo<ProductCategory> _genericRepo;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepo<ProductCategory> genericRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _genericRepo = genericRepo;
        }
        #endregion

        #region CUD

        [HttpGet]
        public async Task<ActionResult<ProductViewModel>> Add()
        {

            var categories = await _genericRepo.GetAllAsync();

            //Get the Categories Seperated not with the product
            var product = new ProductViewModel
            {
                ProductCategories = categories.Select(x => new ProductCategoryViewModel
                {
                    id = x.Id,
                    categoryName = x.categoryName
                }
            )
            };

            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromForm] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {

                var productToAdd = _mapper.Map<Product>(productViewModel); // Map Entity : It's ViewModel
                await _unitOfWork.ProductRepo.AddAsync(productToAdd);
            }

            return RedirectToAction("Privacy", "Home");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            // Already checked " if not null "
            await _unitOfWork.ProductRepo.DeleteAsync(id);
            return View();
        }


        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] ProductViewModel productViewModel)
        {
            var x = _mapper.Map<Product>(productViewModel);
            await _unitOfWork.ProductRepo.UpdateAsync(id, x);

            return View();
        }

        #endregion

        #region Read-GET


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> List()
        {

            var getAllProduct = await _genericRepo.GetAllAsync();
            var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(getAllProduct);

            if (productViewModels == null)
            {
                return NotFound();
            }

            else
                return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var getByIdProduct = await _unitOfWork.ProductRepo.GetByIdAsync(id);
            var productViewModels = _mapper.Map<ProductViewModel>(getByIdProduct);

            if (productViewModels == null)
            { 
                return NotFound(); 
            }

            return View();
        }


        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAllIsActive() // Hold 4n
        {
            var isActiveProducts = await _unitOfWork.ProductRepo.GetAllActiveAsync();
            var productViewModel = _mapper.Map<ProductViewModel>(isActiveProducts);

            if (productViewModel == null)
            {
                return NotFound();
            }
            return View();

        }
        #endregion

        #region IS?
        public async Task<bool> IsActive(int id)
        {
            bool isActiveProduct = await _unitOfWork.ProductRepo.IsActive(id);
            return isActiveProduct;
        }


        public async Task<bool> IsDeActive(int id)
        {
            bool isDeActiveProduct = await _unitOfWork.ProductRepo.IsDeActive(id);
            return isDeActiveProduct;
        }

        public async Task<bool> IsDelete(int id)
        {
            var isDeletedProduct = await _unitOfWork.ProductRepo.IsDeleted(id);
            return isDeletedProduct;
        }


        public IActionResult Index()
        {

            return View();
        }
        #endregion

    }
}
