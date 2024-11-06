using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    public class CategoryController : Controller
    {
        [Route("category/categories")]
        [ActionName("_categories")]
        public IActionResult GetCategories()
        {
            return View("Partials/_categories");
        }
    }
}
