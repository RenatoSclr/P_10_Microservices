using CSharpFunctionalExtensions;
using Frontend.ViewModel.NotesViewModel;

namespace Frontend.Services.Interface
{
    public interface INoteService
    {
        Task<Result<List<NoteSummary>>> GetPatientNotes(Guid patientId, string note);
        Task<Result> CreateNote(CreateNoteViewModel note, string token);
        Task<Result> DeleteNote();
    }
}
