using Microsoft.AspNetCore.Mvc;
using Sample.ASPNETCore.Consumers;
using Sample.ASPNETCore.Models;
using System.Diagnostics;

namespace Sample.ASPNETCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductConsumer _productConsumer;

        public HomeController(ILogger<HomeController> logger,ProductConsumer productConsumer)
        {
            _logger = logger;
            _productConsumer = productConsumer;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productConsumer.GetAllProducts();
            return View(products);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productConsumer.GetSingleProduct(id);
            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}