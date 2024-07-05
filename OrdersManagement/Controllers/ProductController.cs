using DAL.Interfaces;
using DAL.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            
            return View();
        }
    }
}
