using Eye.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eye.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductClientService _productService;
        public ProductController(ILogger<ProductController> logger, IProductClientService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _productService.GetProductsAsync();
            return View(response);
        }
    }
}
