using CSharpFunctionalExtensions;
using Frontend.ViewModel.NotesViewModel;

namespace Frontend.Services.Interface
{
    public interface INoteService
    {
        Task<Result<List<NoteSummary>>> GetPatientNotes(Guid patientId, HttpClient client);
        Task<Result> GetNoteById(Guid patientId);
        Task<Result> CreateNote();
        Task<Result> UpdateNote();
        Task<Result> DeleteNote();
    }
}
