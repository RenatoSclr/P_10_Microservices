using Frontend.Models;
using Frontend.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPatientService _patientService;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IPatientService patientService)
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

        public async Task<IActionResult> Details(Guid Id)
        {
            var response = await _patientService.GetPatientFromPatientAPI(Id);
            if (response.IsSuccessStatusCode)
            {
                return View(await _patientService.DeserializeToPatientDetailsViewModel(response));
            }

            return View("Error");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
