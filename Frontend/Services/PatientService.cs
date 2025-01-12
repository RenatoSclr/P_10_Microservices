using System.Net.Http.Headers;
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

        public async Task<Result<PatientDetailsViewModel>> GetDetailsPatient(Guid id, string token)
        {
            var client = await GetAuthorizedClient(token);

            var response = await client.GetAsync($"/patients/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure<PatientDetailsViewModel>("Error");
            }

            var patientData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PatientDetailsViewModel>(patientData);
        }

        public async Task<Result<CreateOrUpdatePatientViewModel>> GetUpdatePatient(Guid id, string token)
        {
            var client = await GetAuthorizedClient(token);

            var response = await client.GetAsync($"/patients/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure<CreateOrUpdatePatientViewModel>("Error");
            }

            var patientData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CreateOrUpdatePatientViewModel>(patientData);
        }

        public async Task<Result<List<PatientListViewModel>>> GetPatientList(string token)
        {
            var client = await GetAuthorizedClient(token);

            var response = await client.GetAsync("/patients");

            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure<List<PatientListViewModel>>("Error");
            }

            var patientData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<PatientListViewModel>>(patientData);
        }

        public async Task<Result> AddPatient(CreateOrUpdatePatientViewModel patient, string token)
        {
            patient.Adresse ??= "";
            patient.NumeroTelephone ??= "";

            var client = await GetAuthorizedClient(token);
            var content = SerializeToHttpContent(patient);

            var response = await client.PostAsync("/patients", content);

            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure("Error");
            }

            return Result.Success();
        }

        public async Task<Result> UpdatePatient(Guid id, CreateOrUpdatePatientViewModel patient, string token)
        {
            patient.Adresse ??= "";
            patient.NumeroTelephone ??= "";

            var client = await GetAuthorizedClient(token);
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

        private async Task<HttpClient> GetAuthorizedClient(string token)
        {
            var client = _httpClientFactory.CreateClient("Gateway");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
    }
}
