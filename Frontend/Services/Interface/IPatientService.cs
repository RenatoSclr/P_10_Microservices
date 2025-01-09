using CSharpFunctionalExtensions;
using Frontend.ViewModel;

namespace Frontend.Services.Interface
{
    public interface IPatientService
    {
        Task<Result<List<PatientListViewModel>>> GetPatientList();
        Task<Result<PatientDetailsViewModel>> GetDetailsPatient(Guid id);
        Task<Result<CreateOrUpdatePatientViewModel>> GetUpdatePatient(Guid id);
        Task<Result> AddPatient(CreateOrUpdatePatientViewModel patient);
        Task<Result> UpdatePatient(Guid id, CreateOrUpdatePatientViewModel patient);
    }
}
