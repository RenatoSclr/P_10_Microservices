using Frontend.Models;
using Frontend.Services.Interface;
using Frontend.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPatientService _patientService;
        private readonly TokenProvider _tokenProvider;

        public HomeController(ILogger<HomeController> logger, IPatientService patientService, TokenProvider tokenProvider)
        {
            _logger = logger;
            _patientService = patientService;
            _tokenProvider = tokenProvider;
        }

        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["auth_token"];

            if (string.IsNullOrEmpty(token) || !_tokenProvider.IsTokenValid(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var response = await _patientService.GetPatientList(token);

            if (response.IsSuccess)
            {
                return View(response.Value);
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var token = Request.Cookies["auth_token"];

            if (string.IsNullOrEmpty(token) || !_tokenProvider.IsTokenValid(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var response = await _patientService.GetDetailsPatient(id, token);

            if (response.IsSuccess)
            {
                return View(response.Value);
            }

            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            var token = Request.Cookies["auth_token"];

            if (string.IsNullOrEmpty(token) || !_tokenProvider.IsTokenValid(token))
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return View(new CreateOrUpdatePatientViewModel());
            }

            var response = await _patientService.GetUpdatePatient(id.Value, token);
            if (response.IsSuccess)
            {
                return View(response.Value);
            }

            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Guid? id, CreateOrUpdatePatientViewModel patient)
        {
            var token = Request.Cookies["auth_token"];

            if (string.IsNullOrEmpty(token) || !_tokenProvider.IsTokenValid(token))
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                if (id == null || id == Guid.Empty)
                {
                    var response = await _patientService.AddPatient(patient, token);
                    if (response.IsSuccess)
                    {
                        TempData["Message"] = $"Le patient {patient.Nom} {patient.Prenom} a été créé avec succès.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var response = await _patientService.UpdatePatient(id.Value, patient, token);
                    if (response.IsSuccess)
                    {
                        TempData["Message"] = $"Le patient {patient.Nom} {patient.Prenom} a été mis à jour avec succès.";
                        return RedirectToAction("Details", new { Id = id.Value });
                    }
                }
            }

            TempData["ErrorMessage"] = "Une erreur s'est produite lors de l'enregistrement du patient.";
            return View(patient);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
