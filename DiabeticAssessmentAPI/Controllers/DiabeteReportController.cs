using DiabeticAssessmentAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DiabeticAssessmentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiabeteReportController : Controller
    {
        private readonly IDiabeteReportService _diabeteService;
        public DiabeteReportController(IDiabeteReportService diabeteService, INoteService noteService)
        {
            _diabeteService = diabeteService;
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetRapportDiabete(Guid patientId)
        {
            var result = await _diabeteService.GetReportDiabete(patientId);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }
    }
}
