using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PatientsAPI.Domain;
using PatientsAPI.Domain.Dtos;
using PatientsAPI.Services;
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
        public async Task<ActionResult> CreatePatients([FromBody] PatientDTO patients)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _patientService.AddPatient(patients);
            return Ok($"Le patients {patients.Nom} a ete cree avec succee");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPatientsById(Guid id)
        {
            var patients = await _patientService.GetPatientById(id);
            if (patients == null) { return NotFound($"Le patient avec le numero d'identifiant : {id}, est introuvable"); }

            return Ok(patients);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPatients()
        {
            var patientsList = await _patientService.GetAllPatients();
            return Ok(patientsList);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePatients(Guid id, [FromBody] PatientDTO patients)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = await _patientService.GetPatientById(id);
            if (patient == null)
            {
                return NotFound($"Le patient avec l'id {id} est introuvable");
            }

            await _patientService.UpdatePatient(patients, id);
            return Ok($"Le patient {patients.Nom} {patients.Prenom} a ete mis a jour avec succee");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(Guid id)
        {
            var patient = await _patientService.GetPatientById(id);
            if (patient == null)
            {
                return NotFound($"Le patient avec l'id {id} est introuvable");
            }

            await _patientService.DeletePatient(patient);
            return Ok($"Patient {patient.Nom} {patient.Prenom} supprimer avec succee");

        }
    }
}
