using CSharpFunctionalExtensions;
using Frontend.Services.Interface;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using Frontend.Models;
using Frontend.ViewModel.NotesViewModel;

namespace Frontend.Services
{
    public class NoteService : INoteService
    {
        public Task<Result> CreateNote()
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteNote()
        {
            throw new NotImplementedException();
        }

        public Task<Result> GetNoteById(Guid patientId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<NoteSummary>>> GetPatientNotes(Guid patientId, HttpClient client)
        {
            var response = await client.GetAsync($"/note/patient/{patientId}");

            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure<List<NoteSummary>>("Error");
            }

            var patientNotesData = await response.Content.ReadAsStringAsync();
            var patientNote = JsonConvert.DeserializeObject<List<Note>>(patientNotesData);

            return await MapToNoteSummary(patientNote);
        }

        public Task<Result> UpdateNote()
        {
            throw new NotImplementedException();
        }

        private async Task<List<NoteSummary>> MapToNoteSummary(List<Note> notes)
        {
            return notes.Select(note => new NoteSummary
            {
                Contenu = note.Contenu,
                DateCreation = note.DateCreatiom
            }).ToList();
        }
    }
}
