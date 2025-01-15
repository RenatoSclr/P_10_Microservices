using CSharpFunctionalExtensions;
using NotesAPI.Domain;
using NotesAPI.Domain.Dtos;
using NotesAPI.Domain.IRepository;
using NotesAPI.Services.IServices;

namespace NotesAPI.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<Result> AddNote(CreateNoteDTO createNote)
        {
            var note = MapToNoteEntity(createNote);

            if (string.IsNullOrEmpty(note.Contenu))
                return Result.Failure("Le contenu de la note ne peut pas être vide.");

            return await _noteRepository.AddNote(note);
        }

        public async Task<Result> DeleteNote(Guid noteId)
        {
            var noteResult = await _noteRepository.GetNoteById(noteId);
            if (noteResult.IsFailure)
                return Result.Failure(noteResult.Error);

            return await _noteRepository.DeleteNote(noteId);
        }

        public async Task<Result> DeleteAllNotesByPatientId(Guid patientId)
        {
            var result = await _noteRepository.DeleteAllNotesByPatientId(patientId);
            if (result.IsFailure)
                return Result.Failure(result.Error);

            return Result.Success();
        }

        public async Task<Result<NoteDTO>> GetNoteDTOById(Guid id)
        {
            var noteResult = await _noteRepository.GetNoteById(id);

            if (noteResult.IsFailure)
                return Result.Failure<NoteDTO>(noteResult.Error);

            var noteDto = MapToNoteDTO(noteResult.Value);
            return Result.Success(noteDto);
        }

        public async Task<Result<List<NoteDTO>>> GetAllPatientNotesDTO(Guid patientId)
        {
            var notesResult = await _noteRepository.GetAllPatientNotes(patientId);

            if (notesResult.IsFailure)
                return Result.Failure<List<NoteDTO>>(notesResult.Error);

            var noteDtoList = MapToNoteDtoList(notesResult.Value);
            return Result.Success(noteDtoList);
        }

        public async Task<Result> UpdateNote(Guid id, UpdateNoteDTO updateNote)
        {
            var noteResult = await _noteRepository.GetNoteById(id);
            if (noteResult.IsFailure)
                return Result.Failure(noteResult.Error);

            var updatedNote = MapUpdateNoteDtoToNote(updateNote, noteResult.Value);

            return await _noteRepository.UpdateNote(updatedNote);
        }

        private Note MapToNoteEntity(CreateNoteDTO createNote)
        {
            return new Note
            {
                Contenu = createNote.Contenu,
                PatientId = createNote.PatientId,
                NomPatient = createNote.NomPatient,
                DateCreatiom = DateTime.UtcNow
            };
        }

        private Note MapUpdateNoteDtoToNote(UpdateNoteDTO updateNote, Note existingNote)
        {
            existingNote.Contenu = updateNote.Contenu;
            return existingNote;
        }

        private List<NoteDTO> MapToNoteDtoList(List<Note> notes)
        {
            var noteDtoList = new List<NoteDTO>();

            foreach (var note in notes)
            {
                noteDtoList.Add(MapToNoteDTO(note));
            }

            return noteDtoList;
        }

        private NoteDTO MapToNoteDTO(Note note)
        {
            return new NoteDTO
            {
                NoteId = note.NoteId,
                Contenu = note.Contenu,
                NomPatient = note.NomPatient,
                PatientId = note.PatientId,
                DateCreatiom = note.DateCreatiom
            };
        }

        
    }
}
