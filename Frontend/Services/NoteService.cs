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
        private readonly IHttpService _httpService;

        public NoteService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<Result> CreateNote(CreateNoteViewModel createNoteViewModel, string token)
        {
            var note = await MapToNoteEntity(createNoteViewModel);

            var response = await _httpService.PostAsync("/note", note, token);

            if (response.IsFailure)
                return Result.Failure("Erreur lors de la création de la note.");

            return Result.Success();
        }


        public Task<Result> DeleteNote()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<NoteSummary>>> GetPatientNotes(Guid patientId, string token)
        {
            var notesResult = await _httpService.GetAsync<List<Note>>($"/note/patient/{patientId}", token);

            if (notesResult.IsFailure)
                return Result.Failure<List<NoteSummary>>("Erreur lors de la récupération des notes.");

            return await MapToNoteSummary(notesResult.Value);
        }

        private async Task<List<NoteSummary>> MapToNoteSummary(List<Note> notes)
        {
            return notes.Select(note => new NoteSummary
            {
                Contenu = note.Contenu,
                DateCreation = note.DateCreatiom
            }).ToList();
        }

        private async Task<Note> MapToNoteEntity(CreateNoteViewModel noteViewModel)
        {
            return new Note
            {
                PatientId = noteViewModel.PatientId,
                Contenu = noteViewModel.Contenu,
                NomPatient = noteViewModel.NomPatient,
            };
        }
    }
}
