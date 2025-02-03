using CSharpFunctionalExtensions;
using DiabeticAssessmentAPI.Domain;
using DiabeticAssessmentAPI.Services.IServices;
using Newtonsoft.Json;

namespace DiabeticAssessmentAPI.Services
{
    public class NoteService : INoteService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public NoteService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Result<List<ContenuNotePatient>>> GetContenuNotePatientAsync(Guid patientId)
        {
            var client = _httpClientFactory.CreateClient("NoteAPI");
            var response = await client.GetAsync($"note/patient/{patientId}/content");

            if (!response.IsSuccessStatusCode)
                return Result.Failure<List<ContenuNotePatient>>($"Erreur lors de l'appel GET : {response.StatusCode}");

            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ContenuNotePatient>>(data);
            return Result.Success(result);
        }
    }
}
