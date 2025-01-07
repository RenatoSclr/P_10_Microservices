using Frontend.Services.Interface;
using Frontend.ViewModel;
using Newtonsoft.Json;

namespace Frontend.Services
{
    public class PatientService : IPatientService
    {
        private readonly HttpClient _httpClient;
        public PatientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetPatientFromPatientAPI(Guid id)
        {
            return await _httpClient.GetAsync($"https://localhost:5000/patients/{id}");
        }

        public async Task<HttpResponseMessage> GetPatientListFromPatientAPI()
        {
            return await _httpClient.GetAsync("https://localhost:5000/patients");
        }

        public async Task<List<PatientListViewModel>> DeserializeToPatientListViewModel(HttpResponseMessage response) 
        {
            var patientData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<PatientListViewModel>>(patientData);
        }

        public async Task<PatientDetailsViewModel> DeserializeToPatientDetailsViewModel(HttpResponseMessage response)
        {
            var patientData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PatientDetailsViewModel>(patientData);
        }
    }
}
