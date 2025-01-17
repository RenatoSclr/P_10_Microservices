using Microsoft.AspNetCore.Mvc;

namespace DiabeticAssessmentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RapportDiabeteController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult GetRapportDiabete(Guid patientId)
        {
            //Appel Service RapportDiabeteDTO
            return Ok();
        }
    }
}
