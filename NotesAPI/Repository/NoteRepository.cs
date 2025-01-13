using CSharpFunctionalExtensions;
using MongoDB.Driver;
using NotesAPI.Data;
using NotesAPI.Domain;
using NotesAPI.Domain.IRepository;

namespace NotesAPI.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly NoteDbContext _context;

        public NoteRepository(NoteDbContext context)
        {
            _context = context;
        }

        public async Task<Result> AddNote(Note note)
        {
            try
            {
                await _context.Note.InsertOneAsync(note);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while adding the note: {ex.Message}");
            }
        }

        public async Task<Result> DeleteNote(Guid noteId)
        {
            try
            {
                var filter = Builders<Note>.Filter.Eq(n => n.NoteId, noteId);
                var result = await _context.Note.DeleteOneAsync(filter);

                if (result.DeletedCount == 0)
                    return Result.Failure("Note not found");

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while deleting the note: {ex.Message}");
            }
        }

        public async Task<Result<List<Note>>> GetAllPatientNotes(Guid patientId)
        {
            try
            {
                var filter = Builders<Note>.Filter.Eq(n => n.PatientId, patientId);
                var notes = await _context.Note.Find(filter).ToListAsync();

                return Result.Success(notes);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<Note>>($"An error occurred while fetching notes: {ex.Message}");
            }
        }

        public async Task<Result<Note>> GetNoteById(Guid id)
        {
            try
            {
                var filter = Builders<Note>.Filter.Eq(n => n.NoteId, id);
                var note = await _context.Note.Find(filter).FirstOrDefaultAsync();

                if (note == null)
                    return Result.Failure<Note>("Note not found");

                return Result.Success(note);
            }
            catch (Exception ex)
            {
                return Result.Failure<Note>($"An error occurred while fetching the note: {ex.Message}");
            }
        }

        public async Task<Result> UpdateNote(Note note)
        {
            try
            {
                var filter = Builders<Note>.Filter.Eq(n => n.NoteId, note.NoteId);
                var result = await _context.Note.ReplaceOneAsync(filter, note);

                if (result.MatchedCount == 0)
                    return Result.Failure("Note not found");

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while updating the note: {ex.Message}");
            }
        }
    }
}
