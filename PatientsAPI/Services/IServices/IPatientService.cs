using CSharpFunctionalExtensions;
using PatientsAPI.Domain;
using PatientsAPI.Domain.Dtos;

namespace PatientsAPI.Services.IServices
{
    public interface IPatientService
    {
        Task<Result<List<GetPatientDTO>>> GetAllPatients();
        Task<Result<Patient>> GetPatientById(Guid id);
        Task<Result<GetPatientDTO>> GetPatientDTOById(Guid id);
        Task<Result> AddPatient(CreateOrUpdatePatientDTO patient);
        Task<Result> UpdatePatient(CreateOrUpdatePatientDTO patient, Guid id);
        Task<Result> DeletePatient(Patient patient);
        Task<Result<PatientMinimalInfoDTO>> GetPatientMinimalInfoDTOById(Guid id);
    }
}
