using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Kollade på hur webappen autentiserar sig och försökte replikera det nedan(antar vi gör samma här?).
        // Innebär att det som sker nedan inte riktigt överrensstämmer med user storyn.

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            // Om appen autentiserar sig med 
            // Glöm inte ta bort uttropstecken när logiken implementeras!

            if (!User.Identity!.IsAuthenticated)
            {
                return View();
            }


            // Ändra till rätt kontrollernamn beroende på hur det implementeras?

            return RedirectToAction("login", "home");

           
        }

        
        // Varför inte ha all inloggningsfunktion i denna controller, sen sätta authorize på hela controllern i dom andra?


        [Route("/home/signoutasync")]
        public IActionResult SignOutAsync()
        {

            HttpContext.SignOutAsync();

            // Ändra till rätt kontrollernamn beroende på hur det implementeras?

            return RedirectToAction("login", "home");
        }

    }
}
