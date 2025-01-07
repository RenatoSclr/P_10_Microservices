using System.Text;
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

        public async Task<HttpResponseMessage> AddPatientToPatientAPI(CreateOrUpdatePatientViewModel patient)
        {
            var content = SerializeToHttpContent(patient);
            return await _httpClient.PostAsync("https://localhost:5000/patients", content);
        }

        public async Task<HttpResponseMessage> UpdatePatientInPatientAPI(Guid id, CreateOrUpdatePatientViewModel patient)
        {
            var content = SerializeToHttpContent(patient);
            return await _httpClient.PutAsync($"https://localhost:5000/patients/{id}", content);
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

        public async Task<CreateOrUpdatePatientViewModel> DeserializeToCreateOrUpdatePatientViewModel(HttpResponseMessage response)
        {
            var patientData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CreateOrUpdatePatientViewModel>(patientData);
        }

        private HttpContent SerializeToHttpContent(CreateOrUpdatePatientViewModel patientToUpsert)
        {
            var jsonData = JsonConvert.SerializeObject(patientToUpsert);
            return new StringContent(jsonData, Encoding.UTF8, "application/json");
        }
    }
}
