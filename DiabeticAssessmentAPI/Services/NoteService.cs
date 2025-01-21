using CSharpFunctionalExtensions;
using DiabeticAssessmentAPI.Domain.Dtos;
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
        public async Task<Result<List<ContenuNotePatientDTO>>> GetContenuNotePatientAsync(Guid patientId)
        {
            var client = _httpClientFactory.CreateClient("NoteAPI");
            var response = await client.GetAsync($"note/patient/{patientId}/content");

            if (!response.IsSuccessStatusCode)
                return Result.Failure<List<ContenuNotePatientDTO>>($"Erreur lors de l'appel GET : {response.StatusCode}");

            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ContenuNotePatientDTO>>(data);
            return Result.Success(result);
        }
    }
}
