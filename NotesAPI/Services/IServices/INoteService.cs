using CSharpFunctionalExtensions;
using NotesAPI.Domain;
using NotesAPI.Domain.Dtos;

namespace NotesAPI.Services.IServices
{
    public interface INoteService
    {
        Task<Result<NoteDTO>> GetNoteDTOById(Guid id);
        Task<Result<List<NoteDTO>>> GetAllPatientNotesDTO(Guid PatientId);
        Task<Result> AddNote(CreateNoteDTO note);
        Task<Result> DeleteNote(Guid id);
        Task<Result> UpdateNote(Guid id, UpdateNoteDTO note);
        Task<Result> DeleteAllNotesByPatientId(Guid patientId);
    }
}
