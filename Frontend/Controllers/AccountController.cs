using Frontend.Services.Interface;
using Frontend.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _authService.GetToken(request);

            if (result.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nom d'utilisateur ou mot de passe invalide.");
                return View(request);
            }
        }
    }
}
