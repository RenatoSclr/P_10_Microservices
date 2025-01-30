using CSharpFunctionalExtensions;
using DiabeticAssessmentAPI.Domain;
using DiabeticAssessmentAPI.Services.IServices;
using Newtonsoft.Json;

namespace DiabeticAssessmentAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public PatientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Result<InfoPatientDTO>> GetInfoPatientAsync(Guid patientId)
        {
            var client = _httpClientFactory.CreateClient("PatientAPI");
            var response = await client.GetAsync($"/patients/{patientId}/minimal-info");

            if (!response.IsSuccessStatusCode)
                return Result.Failure<InfoPatientDTO>($"Erreur lors de l'appel GET : {response.StatusCode}");

            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<InfoPatientDTO>(data);
            return Result.Success(result);
        }
    }
}
