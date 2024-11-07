using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    public class UserController : Controller
    {

        [Route("user/users")]
        [ActionName("_users")]
        public IActionResult GetUsers()
        {
            return View("Partials/_users");
        }
    }
}
