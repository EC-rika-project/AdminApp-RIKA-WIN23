using AdminApp.Factories;
using AdminApp.ViewModels;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    public class SignInController(IAppAuthenticationService appAuthenticationService) : Controller
    {

        private readonly IAppAuthenticationService _authService = appAuthenticationService;


        [Route("/signin")]
        public IActionResult Signin()
        {
            return View();
        }


        [HttpPost]
        [Route("/signin")]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                var signInDto = SignInFactory.CreateSignInDto(viewModel);
                var token = await _authService.SignInAsync(signInDto);

                if (token != null)
                {
                    await _authService.SignInUserWithTokenAsync(token, signInDto.RememberMe);
                    TempData["Message"] = "Successfully logged in!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("Index", "Home");
                }

                TempData["Message"] = "Login failed. Please try again.";
                TempData["MessageType"] = "error";
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"An error occurred: {ex.Message}";
                TempData["MessageType"] = "error";
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(viewModel);


        }


    }
}
