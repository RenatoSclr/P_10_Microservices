using CSharpFunctionalExtensions;
using PatientsAPI.Domain;

namespace PatientsAPI.Domain.IRepository
{
    public interface IPatientRepository
    {
        Task<Result<List<Patient>>> GetAllPatients();
        Task<Result<Patient>> GetPatients(Guid id);
        Task<Result> AddPatient(Patient patient);
        Task<Result> UpdatePatient(Patient patient);
        Task<Result> DeletePatient(Patient patient);
        Task<Result> Save();
    }
}
