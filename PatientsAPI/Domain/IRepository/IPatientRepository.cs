using PatientsAPI.Domain;

namespace PatientsAPI.Domain.IRepository
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatients();
        Task<Patient> GetPatients(Guid id);
        Task AddPatient(Patient patient);
        Task UpdatePatient(Patient patient);
        Task DeletePatient(Patient patient);
        Task Save();
    }
}
