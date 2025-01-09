using System.Text;
using CSharpFunctionalExtensions;
using Frontend.Services.Interface;
using Frontend.ViewModel;
using Newtonsoft.Json;

namespace Frontend.Services
{
    public class PatientService : IPatientService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PatientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private async Task<HttpClient> GetClient()
        {
            return _httpClientFactory.CreateClient("PatientAPI");
        }

        public async Task<Result<PatientDetailsViewModel>> GetDetailsPatient(Guid id)
        {
            var client = await GetClient();
            var response = await client.GetAsync($"/patients/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure<PatientDetailsViewModel>("Error");
            }
            var patientData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PatientDetailsViewModel>(patientData);
        }

        public async Task<Result<CreateOrUpdatePatientViewModel>> GetUpdatePatient(Guid id)
        {
            var client = await GetClient();
            var response = await client.GetAsync($"/patients/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure<CreateOrUpdatePatientViewModel>("Error");
            }
            var patientData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CreateOrUpdatePatientViewModel>(patientData);
        }

        public async Task<Result<List<PatientListViewModel>>> GetPatientList()
        {
            var client = await GetClient();
            var response = await client.GetAsync("/patients");
            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure<List<PatientListViewModel>>("Error");
            }
            var patientData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<PatientListViewModel>>(patientData);
        }

        public async Task<Result> AddPatient(CreateOrUpdatePatientViewModel patient)
        {
            var client = await GetClient();
            var content = SerializeToHttpContent(patient);
            var response = await client.PostAsync("/patients", content);
            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure("Error");
            }
            return Result.Success();
        }

        public async Task<Result> UpdatePatient(Guid id, CreateOrUpdatePatientViewModel patient)
        {
            var client = await GetClient();
            var content = SerializeToHttpContent(patient);
            var response = await client.PutAsync($"/patients/{id}", content);
            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure("Error");
            }
            return Result.Success();
        }


        private HttpContent SerializeToHttpContent(CreateOrUpdatePatientViewModel patientToUpsert)
        {
            var jsonData = JsonConvert.SerializeObject(patientToUpsert);
            return new StringContent(jsonData, Encoding.UTF8, "application/json");
        }
    }
}
