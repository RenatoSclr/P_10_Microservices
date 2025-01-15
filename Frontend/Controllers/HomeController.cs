using Frontend.Models;
using Frontend.Services.Interface;
using Frontend.ViewModel.PatientViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly ITokenProvider _tokenProvider;

        public HomeController(IPatientService patientService, ITokenProvider tokenProvider)
        {
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

            if (!id.HasValue)
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
                        TempData["Message"] = $"Le patient {patient.Nom} {patient.Prenom} a �t� cr�� avec succ�s.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var response = await _patientService.UpdatePatient(id.Value, patient, token);
                    if (response.IsSuccess)
                    {
                        TempData["Message"] = $"Le patient {patient.Nom} {patient.Prenom} a �t� mis � jour avec succ�s.";
                        return RedirectToAction("Details", new { Id = id.Value });
                    }
                }
            }
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePatient(Guid patientId)
        {
            var token = Request.Cookies["auth_token"];

            if (string.IsNullOrEmpty(token) || !_tokenProvider.IsTokenValid(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await _patientService.DeletePatient(patientId, token);

            if (result.IsFailure)
            {
                TempData["Message"] = "�chec de la suppression du patient. " + result.Error;
            }
            else
            {
                TempData["Message"] = "Patient supprim� avec succ�s.";
            }

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
