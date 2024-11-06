using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    public class ProductController : Controller
    {
        [Route("product/products")]
        [ActionName("_products")]
        public IActionResult GetProducts()
        {
            return View("Partials/_products");
        }
    }
}
