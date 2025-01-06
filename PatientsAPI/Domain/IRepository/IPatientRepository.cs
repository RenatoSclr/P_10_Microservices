using Patient.Domain;

namespace Patient.Domain.IRepository
{
    public interface IPatientRepository
    {
        Task<List<Patients>> GetAllPatients();
        Task<Patients> GetPatients(int id);
        Task AddPatient(Patients patient);
        Task UpdatePatient(Patients patient);
        Task DeletePatient(Patients patient);
        Task Save();
    }
}
