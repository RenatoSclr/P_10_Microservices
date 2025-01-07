using Frontend.ViewModel;

namespace Frontend.Services.Interface
{
    public interface IPatientService
    {
        Task<HttpResponseMessage> GetPatientListFromPatientAPI();
        Task<HttpResponseMessage> GetPatientFromPatientAPI(Guid id);
        Task<HttpResponseMessage> AddPatientToPatientAPI(CreateOrUpdatePatientViewModel patient);
        Task<HttpResponseMessage> UpdatePatientInPatientAPI(Guid id, CreateOrUpdatePatientViewModel patient);
        Task<List<PatientListViewModel>> DeserializeToPatientListViewModel(HttpResponseMessage response);
        Task<PatientDetailsViewModel> DeserializeToPatientDetailsViewModel(HttpResponseMessage response);
        Task<CreateOrUpdatePatientViewModel> DeserializeToCreateOrUpdatePatientViewModel(HttpResponseMessage response);

    }
}
