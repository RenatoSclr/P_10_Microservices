using PatientsAPI.Domain;
using PatientsAPI.Domain.Dtos;

namespace PatientsAPI.Services.IServices
{
    public interface IPatientService
    {
        Task<List<PatientDTO>> GetAllPatients();
        Task<Patient> GetPatientById(Guid id);
        Task<PatientDTO> GetPatientDTOById(Guid id);
        Task AddPatient(PatientDTO patient);
        Task UpdatePatient(PatientDTO patient, Guid id);
        Task DeletePatient(Patient patient);
    }
}
