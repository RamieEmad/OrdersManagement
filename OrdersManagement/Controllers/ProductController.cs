using AutoMapper;
using BLL.Interfaces;
using PL.Mapping;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics.Eventing.Reader;

namespace PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var productToAdd = _mapper.Map<Product>(productViewModel); // Map Entity : It's ViewModel
                await _unitOfWork.ProductRepo.AddAsync(productToAdd);
            }

            return RedirectToAction();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            // Already checked " if not null "
            await _unitOfWork.ProductRepo.DeleteAsync(id);
            return View();
        }

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

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] ProductViewModel productViewModel)
        {
            var x = _mapper.Map<Product>(productViewModel);
            await _unitOfWork.ProductRepo.UpdateAsync(id, x);

            return View();
        }


        public async Task<ActionResult<IEnumerable<ProductViewModel>>> IsActive()
        {
            var isActiveProducts = await _unitOfWork.ProductRepo.GetAllActiveAsync();
            return Ok(isActiveProducts);
           
        }


        public async Task<IActionResult> SoftDelete(int id)
        {
            var getProductById = await _unitOfWork.ProductRepo.GetByIdAsync(id);
            if (getProductById == null)
                return NotFound();

            await _unitOfWork.ProductRepo.IsDeleted(getProductById);
            return RedirectToAction();
            
        }


        public IActionResult Index()
        {

            return View();
        }
    }
}
