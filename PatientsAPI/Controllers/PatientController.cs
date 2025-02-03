using Microsoft.AspNetCore.Mvc;
using PatientsAPI.Dtos;
using PatientsAPI.Services.IServices;

namespace PatientsAPI.Controllers
{
    [ApiController]
    [Route("patients")]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePatients([FromBody] CreateOrUpdatePatientDTO patients)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _patientService.AddPatient(patients);
            return result.IsSuccess ? Ok(patients) : BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPatientsById(Guid id)
        {
            var result = await _patientService.GetPatientDTOById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPatients()
        {
            var result = await _patientService.GetAllPatients();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpGet("{id}/minimal-info")]
        public async Task<ActionResult> GetMinimalInfo(Guid id)
        {
            var result = await _patientService.GetPatientMinimalInfoDTOById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePatients(Guid id, [FromBody] CreateOrUpdatePatientDTO patients)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultPatient = await _patientService.GetPatientById(id);

            if (resultPatient.IsFailure)
                return NotFound(resultPatient.Error);

            var result = await _patientService.UpdatePatient(patients, id);
            return result.IsSuccess ? Ok(patients) : BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(Guid id)
        {
            var resultpatient = await _patientService.GetPatientById(id);
            if (resultpatient.IsFailure)
                return NotFound(resultpatient.Error);

            var result = await _patientService.DeletePatient(resultpatient.Value);
            return result.IsSuccess ? Ok($"Patient {resultpatient.Value.Nom} {resultpatient.Value.Prenom} supprimer avec succee"): BadRequest(result.Error);  

        }
    }
}
