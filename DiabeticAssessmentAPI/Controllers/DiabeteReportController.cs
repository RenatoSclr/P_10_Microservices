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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRapportDiabete(Guid id)
        {
            var result = await _diabeteService.GetReportDiabete(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }
    }
}
