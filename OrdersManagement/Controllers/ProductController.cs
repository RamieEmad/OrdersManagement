using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using System.Linq;

namespace PL.Controllers
{
    public class ProductController : Controller
    {
        #region DI
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region CUD

        [HttpGet]
        public async Task<ActionResult<ProductViewModel>> Add()
        {
            var products = await _unitOfWork.ProductRepo.GetAllAsync();
            
            var productCategories = products.Select(c => new ProductCategoryViewModel
            {
                id = c.productCategories.Id,
                categoryName = c.productCategories.categoryName
            }).ToList();

            return new ProductViewModel { ProductCategoryViewModel = productCategories };

        }

         
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var productToAdd = _mapper.Map<Product>(productViewModel); // Map Entity : It's ViewModel
                await _unitOfWork.ProductRepo.AddAsync(productToAdd);
            }



            //var productTasks = await _unitOfWork.ProductRepo.GetAllAsync();

            //var productViewModel = productTasks.Select(c => new ProductCategoryViewModel
            //{
            //    id = c.productCategories.Id,
            //    categoryName = c.productCategories.categoryName
            //}).ToList();


            return View(productViewModel);
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
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAll()
        {
            var getAllProduct = await _unitOfWork.ProductRepo.GetAllAsync();

            if (getAllProduct == null)
            {
                return NotFound();
            }

            else
                return Ok(getAllProduct);
        }


        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var getByIdProduct = await _unitOfWork.ProductRepo.GetByIdAsync(id);
            return Ok(getByIdProduct);
        }




        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAllIsActive() // Hold 4n
        {
            var isActiveProducts = await _unitOfWork.ProductRepo.GetAllActiveAsync();
            return Ok(isActiveProducts);

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
