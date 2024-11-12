using AdminApp.ViewModels;
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
                var stats = GetDashboardStatistics();

                return View(stats);
            }


            // Ändra till rätt kontrollernamn beroende på hur det implementeras?

            return View();

           
        }

        
        // Varför inte ha all inloggningsfunktion i denna controller, sen sätta authorize på hela controllern i dom andra?


        [Route("/home/signoutasync")]
        public IActionResult SignOutAsync()
        {

            HttpContext.SignOutAsync();

            // Ändra till rätt kontrollernamn beroende på hur det implementeras?

            return RedirectToAction("login", "home");
        }



        private HomeViewModel GetDashboardStatistics()
        {
            // Mock data - In a real application, this would come from a database or service.
            return new HomeViewModel
            {
                TotalUsers = 1024,
                ActiveSessions = 256,
                TotalSales = 34500.75m,
                NewOrders = 45,
                PendingTasks = 7
            };
        }
    }
}
