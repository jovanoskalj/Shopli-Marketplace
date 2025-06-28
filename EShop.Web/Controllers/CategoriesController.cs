using EShop.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ISearchService _searchService;

        public CategoriesController(ICategoryService categoryService, ISearchService searchService)
        {
            _categoryService = categoryService;
            _searchService = searchService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetActiveCategoriesAsync();
            return View(categories);
        }

        // GET: Categories/Products/5
        public async Task<IActionResult> Products(Guid id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            var products = await _categoryService.GetProductsByCategoryAsync(id);
            ViewBag.CategoryName = category.Name;
            ViewBag.CategoryId = id;
            
            return View(products);
        }
        
        // Diagnostic action to test category product retrieval
        [HttpGet]
        public async Task<IActionResult> TestCategoryProducts(Guid id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return Json(new { error = "Category not found", categoryId = id });
            }

            var products = await _categoryService.GetProductsByCategoryAsync(id);
            
            return Json(new {
                CategoryId = id,
                CategoryName = category.Name,
                ProductCount = products.Count,
                Products = products.Select(p => new {
                    Id = p.Id,
                    Name = p.ProductName,
                    CategoryId = p.CategoryId,
                    IsActive = p.IsActive
                }).ToList()
            });
        }
    }
}
