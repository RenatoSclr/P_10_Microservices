using CSharpFunctionalExtensions;
using DiabeticAssessmentAPI.Domain.Dtos;
using DiabeticAssessmentAPI.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http;

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
            return Result.Success(JsonConvert.DeserializeObject<List<ContenuNotePatientDTO>>(data));
        }
    }
}
