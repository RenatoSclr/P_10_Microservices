using Patient.Domain;

namespace Patient.Services.IServices
{
    public interface IPatientService
    {
        Task<List<Patients>> GetAllPatients();
        Task<Patients> GetPatientById(int id);
        Task AddPatient(Patients patient);
        Task UpdatePatient(Patients patient);
        Task DeletePatient(Patients patient);
    }
}
