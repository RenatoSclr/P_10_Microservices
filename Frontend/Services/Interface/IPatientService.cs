using CSharpFunctionalExtensions;
using Frontend.Models;
using Frontend.ViewModel;
using Frontend.ViewModel.PatientViewModel;

namespace Frontend.Services.Interface
{
    public interface IPatientService
    {
        Task<Result<List<PatientListViewModel>>> GetPatientList(string token);
        Task<Result<PatientDetailsViewModel>> GetDetailsPatient(Guid id, string token);
        Task<Result<CreateOrUpdatePatientViewModel>> GetUpdatePatient(Guid id, string token);
        Task<Result> AddPatient(CreateOrUpdatePatientViewModel patient, string token);
        Task<Result> UpdatePatient(Guid id, CreateOrUpdatePatientViewModel patient, string token);
    }
}
