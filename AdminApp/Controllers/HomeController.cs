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

        // Kollade p� hur webappen autentiserar sig och f�rs�kte replikera det nedan(antar vi g�r samma h�r?).
        // Inneb�r att det som sker nedan inte riktigt �verrensst�mmer med user storyn.

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            // Om appen autentiserar sig med 
            // Gl�m inte ta bort uttropstecken n�r logiken implementeras!

            if (!User.Identity!.IsAuthenticated)
            {
                var stats = GetDashboardStatistics();

                return View(stats);
            }


            // �ndra till r�tt kontrollernamn beroende p� hur det implementeras?

            return View();

           
        }

        
        // Varf�r inte ha all inloggningsfunktion i denna controller, sen s�tta authorize p� hela controllern i dom andra?


        [Route("/home/signoutasync")]
        public IActionResult SignOutAsync()
        {

            HttpContext.SignOutAsync();

            // �ndra till r�tt kontrollernamn beroende p� hur det implementeras?

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
