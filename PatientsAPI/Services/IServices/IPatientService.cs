using PatientsAPI.Domain;
using PatientsAPI.Domain.Dtos;

namespace PatientsAPI.Services.IServices
{
    public interface IPatientService
    {
        Task<List<GetPatientDTO>> GetAllPatients();
        Task<Patient> GetPatientById(Guid id);
        Task<GetPatientDTO> GetPatientDTOById(Guid id);
        Task AddPatient(CreateOrUpdatePatientDTO patient);
        Task UpdatePatient(CreateOrUpdatePatientDTO patient, Guid id);
        Task DeletePatient(Patient patient);
        Task<PatientMinimalInfoDTO> GetPatientMinimalInfoDTOById(Guid id);
    }
}
