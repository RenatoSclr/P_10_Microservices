using PatientsAPI.Domain;

namespace PatientsAPI.Services.IServices
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatients();
        Task<Patient> GetPatientById(int id);
        Task AddPatient(Patient patient);
        Task UpdatePatient(Patient patient);
        Task DeletePatient(Patient patient);
    }
}
