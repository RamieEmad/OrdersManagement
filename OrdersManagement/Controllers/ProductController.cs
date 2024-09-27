using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PL.Models;
using System.Data;

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
        private readonly IWebHostEnvironment _webHost;
        private readonly IUploadFileRepo _uploadFileRepo;


        public ProductController
            (IUnitOfWork unitOfWork,
            IMapper mapper,
            IGenericRepo<ProductCategory> genericRepo,
            ILogger<ProductController> logger,
            IGenericRepo<Product> productRepo,
            IWebHostEnvironment webHost,
            IUploadFileRepo uploadFileRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _generiCategory = genericRepo;
            _logger = logger;
            _genericProduct = productRepo;
            _webHost = webHost;
            _uploadFileRepo = uploadFileRepo;   
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
        public async Task<ActionResult> Add(ProductViewModel productViewModel, UploadFileViewModel uploadFileViewModel)
        {
            if (productViewModel == null)
            {
                return NotFound();
            }

            Product product = new Product
            {
                prodName = productViewModel.prodName,
                prodDesc = productViewModel.prodDesc,
                ProductCategoryId = productViewModel.ProductCategoryId,
                IsActive = productViewModel.IsActive,
                IsDeleted = productViewModel.IsDeleted,
                
            };

            var productToAdd = _mapper.Map<Product>(product);
            await _genericProduct.AddAsync(productToAdd);

            #region Upload-File
            if (uploadFileViewModel != null && uploadFileViewModel.File.Length > 0)
            {

                // If a file is uploaded, save it to the images directory
                if (uploadFileViewModel.File != null)
                {
                    // Create a directory for product images if it doesn't exist
                    string imagesDir = Path.Combine(_webHost.WebRootPath, "Product", "Images");
                    if (!Directory.Exists(imagesDir))
                    {
                        Directory.CreateDirectory(imagesDir);
                    }

                    // Save the file to the images directory
                        
                    string filePath = Path.Combine(imagesDir, uploadFileViewModel.File.FileName);

                    // Get the relative path from the web root path to the file path
                    string relativePath = Path.GetRelativePath(_webHost.WebRootPath, filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        uploadFileViewModel.File.CopyTo(stream);
                    }

                    // Create a new UploadFile entity
                    UploadFile uploadFile = new UploadFile
                    {
                        ProductId = productToAdd.Id, 
                        ImageUrl = $"/{relativePath}",/*$"/Product/Images/{Guid.NewGuid()}{uploadFileViewModel.File.FileName}",*/
                        FileName = uploadFileViewModel.File.FileName,
                        ContentType =  Path.GetExtension(uploadFileViewModel.File.FileName).ToLower()
                    };

                    //saving
                    await _unitOfWork.UploadFileRepo.AddAsync(uploadFile);

                }
            }
            #endregion

            return Redirect("~/Product/List");

        } 
            #endregion
        
        #region Delete & Confirmation & Delete int-Array
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
            return RedirectToAction("List");
        }

        //Deleting Array
        [HttpPost]
        public async Task<IActionResult> DeleteProducts(int[] productIds)
        {
            foreach (var productId in productIds)
            {
                var product = await _genericProduct.GetByIdAsync(productId);
                await _genericProduct.DeleteArray(product.Id);

            }

            return Json(new { success = true });
        }

        #endregion

        #region Update
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
        public async Task<IActionResult> Update(int id, [FromForm] ProductViewModel viewModel, [FromForm]UploadFileViewModel uploadFileViewModel)
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

            #region Upload-File
            if (uploadFileViewModel != null && uploadFileViewModel.File.Length > 0)
            {
                try
                {
                    // Create a directory for product images if it doesn't exist
                    string imagesDir = Path.Combine(_webHost.WebRootPath, "Product", "Images");
                    if (!Directory.Exists(imagesDir))
                    {
                        Directory.CreateDirectory(imagesDir);
                    }

                    // Get the relative path from the web root path to the file path
                    string filePath = Path.Combine(imagesDir, uploadFileViewModel.File.FileName);

                    // Save the file to the specified path
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        uploadFileViewModel.File.CopyTo(stream);
                    }

                    // Get the relative path from the web root path to the file path
                    string relativePath = Path.GetRelativePath(_webHost.WebRootPath, filePath);

                    // Retrieve all existing images for the product from the database
                    var existingImages = await _uploadFileRepo.GetUploadFilesByProductId(product.Id);

                    // Clear existing images
                    if (existingImages.Any())
                    {
                        foreach (var existingImage in existingImages)
                        {
                            // Delete the existing image file
                            string existingImagePath = Path.Combine(_webHost.WebRootPath, existingImage.ImageUrl.TrimStart('/'));
                            if (System.IO.File.Exists(existingImagePath))
                            {
                                System.IO.File.Delete(existingImagePath);
                            }

                            // Remove the existing image from the database
                            await _unitOfWork.UploadFileRepo.DeleteAsync(existingImage.Id);
                        }
                    }

                    // Create a new UploadFile entity
                    UploadFile uploadFile = new UploadFile
                    {
                        ProductId = product.Id,
                        ImageUrl = $"/{relativePath}",
                        FileName = uploadFileViewModel.File.FileName,
                        ContentType = Path.GetExtension(uploadFileViewModel.File.FileName).ToLower()
                    };

                    // Save the new UploadFile entity to the database
                    await _unitOfWork.UploadFileRepo.AddAsync(uploadFile);
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    _logger.LogError(ex, "Error uploading file");
                }
            }
            #endregion

            return RedirectToAction("List");
        }

        #endregion

        #endregion

        #region LIST & GET
        [HttpGet]
        public ActionResult List(string sortOrder, string searchString, int? pageNumber= 1)
        {
            #region ViewData & ProductListWithCategory

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "prodName_desc" : "";
            ViewData["IsActiveSortParm"] = sortOrder == "IsActive" ? "IsActive_desc" : "IsActive";
            ViewData["CurrentFilter"] = searchString;

            var getAllProduct = _unitOfWork.ProductRepo.GetAllProductWithCategory();

            #endregion

            #region Sorting
            switch (sortOrder)
            {
                case "prodName_desc":
                    getAllProduct = getAllProduct.OrderByDescending(p => p.prodName);
                    break;

                case "prodName":
                    getAllProduct = getAllProduct.OrderBy(p => p.prodName);
                    break;

                case "IsActive_desc":
                    getAllProduct = getAllProduct.OrderByDescending(p => p.IsActive);
                    break;

                case "IsActive":
                    getAllProduct = getAllProduct.OrderBy(p => p.IsActive);
                    break;

                default:
                    getAllProduct = getAllProduct.OrderBy(p => p.prodName);
                    break;
            }
            #endregion

            #region Searching
            if (!String.IsNullOrEmpty(searchString))
            {
                getAllProduct = getAllProduct.Where(p => p.prodName.Contains(searchString));

            }
            #endregion

            #region Pagination & Foreach-ToShowImage
            int pageSize = 5;
            
            var productViewModels = _mapper.Map<List<ProductViewModel>>(getAllProduct);

            // Retrieve the uploaded files for each product
            foreach (var productViewModel in productViewModels)
            {
                var uploadFiles = _unitOfWork.UploadFileRepo.GetUploadFilesByProductId(productViewModel.Id).Result;
                var uploadFileViewModels = _mapper.Map<List<UploadFileViewModel>>(uploadFiles);
                productViewModel.UploadFilesViewModel = uploadFileViewModels;
            }

            return View(PaginatedList<ProductViewModel>.Create(productViewModels,
                   pageNumber ?? 1, pageSize));


            #endregion
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

        #region Details

        public async Task<IActionResult> Details(int id)
        {
            var getByIdProduct = await _genericProduct.GetByIdAsync(id);
            var productViewModel = _mapper.Map<ProductViewModel>(getByIdProduct);
            return View(productViewModel);
        }
        #endregion

        #region IS?
        public IActionResult ToggleActive(int productId)
        {
            var product = _unitOfWork.ProductRepo.GetById(productId);

            if (product != null)
            {
                product.IsActive = !product.IsActive;
                _genericProduct.UpdateAsync(product);

                return Json
                    (new
                    {
                        success = true,
                        product = new { id = product.Id, isActive = product.IsActive },
                        redirectUrl = Url.Action("List", "Product")
                    });

            }

            return RedirectToAction("List");
        }

        #endregion
    }
}


