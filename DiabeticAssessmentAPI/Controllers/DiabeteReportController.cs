using DiabeticAssessmentAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DiabeticAssessmentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiabeteReportController : Controller
    {
        private readonly IDiabeteReportService _diabeteService;
        private readonly INoteService _niabeteService;
        public DiabeteReportController(IDiabeteReportService diabeteService, INoteService noteService)
        {
            _diabeteService = diabeteService;
            _niabeteService = noteService;   
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetRapportDiabete(Guid patientId)
        {
            var result = await _niabeteService.GetContenuNotePatientAsync(patientId);
            Console.WriteLine();
            return Ok(result.Value);
        }
    }
}
