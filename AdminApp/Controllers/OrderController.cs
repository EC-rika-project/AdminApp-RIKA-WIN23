using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    public class OrderController : Controller
    {
        [Route("order/orders")]
        [ActionName("_orders")]
        public IActionResult GetOrders()
        {
            return View("Partials/_orders");
        }
    }
}
