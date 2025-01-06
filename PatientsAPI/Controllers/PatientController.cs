using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PatientsAPI.Domain;
using PatientsAPI.Services;
using PatientsAPI.Services.IServices;

namespace PatientsAPI.Controllers
{
    [ApiController]
    [Route("patients")]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreatePatients(Patient patients)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _patientService.AddPatient(patients);
            return Ok($"Le patients {patients.Nom} a ete cree avec succee");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPatientsById(int patientId)
        {
            var patients = await _patientService.GetPatientById(patientId);
            if (patients == null) { return NotFound($"Le patient avec le numero d'identifiant : {patientId}, est introuvable"); }

            return Ok(patients);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPatients()
        {
            var patientsList = await _patientService.GetAllPatients();
            return Ok(patientsList);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdatePatients(Patient patients)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _patientService.UpdatePatient(patients);
            return Ok($"Le patient {patients.Nom} {patients.Prenom} a ete mis a jour avec succee");
        }

        [HttpPost("delete/{id}")]
        public async Task<ActionResult> DeletePatient(int id)
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
