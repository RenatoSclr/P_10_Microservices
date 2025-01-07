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

        public HomeController(ILogger<HomeController> logger, IPatientService patientService)
        {
            _logger = logger;
            _patientService = patientService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _patientService.GetPatientListFromPatientAPI();
            if (response.IsSuccessStatusCode)
            {
                return View(await _patientService.DeserializeToPatientListViewModel(response));
            }

            return View("Error");
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _patientService.GetPatientFromPatientAPI(id);
            if (response.IsSuccessStatusCode)
            {
                return View(await _patientService.DeserializeToPatientDetailsViewModel(response));
            }

            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            if (id == null)
            {
                return View(new CreateOrUpdatePatientViewModel());
            }
            else
            {
                var response = await _patientService.GetPatientFromPatientAPI(id.Value);
                if (response.IsSuccessStatusCode)
                {
                    return View(await _patientService.DeserializeToCreateOrUpdatePatientViewModel(response));
                }
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Guid? id, CreateOrUpdatePatientViewModel patient)
        {
            if (ModelState.IsValid)
            {
                if (id == null || id == Guid.Empty) 
                {
                    var response = await _patientService.AddPatientToPatientAPI(patient);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = $"Le patient {patient.Nom} {patient.Prenom} a été créé avec succès.";
                        return RedirectToAction("Index");
                    }
                }
                else 
                {
                    var response = await _patientService.UpdatePatientInPatientAPI(id.Value, patient);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = $"Le patient {patient.Nom} {patient.Prenom} a été mis à jour avec succès.";
                        return RedirectToAction("Details", new { Id = id.Value });
                    }
                }
            }

            return View(patient); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
