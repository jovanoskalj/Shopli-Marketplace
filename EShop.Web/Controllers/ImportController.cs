using EShop.Service.Implementation;
using EShop.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly IProductImportService _importService;
        private readonly IProductService _productService;

        public ImportController(IProductImportService importService, IProductService productService)
        {
            _importService = importService;
            this._productService = productService;
        }
        [HttpPost]
        public async Task<IActionResult> Import()
        {
            try
            {
                var courses = await _productService.GetCoursesAsync();

                foreach (var course in courses)
                {
                    var existing = _productService.GetById(course.Id);
                    if (existing == null)
                    {
                        _productService.Insert(course);
                    }
                }

                return Ok("Курсевите се успешно увезени.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Грешка при увоз: " + ex.Message);
            }
        }

    }
}
