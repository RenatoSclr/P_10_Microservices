using CSharpFunctionalExtensions;

namespace NotesAPI.Domain.IRepository
{
    public interface INoteRepository
    {
        Task<Result<List<Note>>> GetAllPatientNotes(Guid patientId);
        Task<Result<Note>> GetNoteById(Guid id);
        Task<Result> AddNote(Note note);
        Task<Result> UpdateNote(Note note);
        Task<Result> DeleteNote(Guid id);
    }
}
