using Frontend.ViewModel;

namespace Frontend.Services.Interface
{
    public interface IPatientService
    {
        Task<HttpResponseMessage> GetPatientListFromPatientAPI();
        Task<HttpResponseMessage> GetPatientFromPatientAPI(Guid id);
        Task<List<PatientListViewModel>> DeserializeToPatientListViewModel(HttpResponseMessage response);
        Task<PatientDetailsViewModel> DeserializeToPatientDetailsViewModel(HttpResponseMessage response);
    }
}
