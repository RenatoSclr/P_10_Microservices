using Microsoft.AspNetCore.Mvc;
using NotesAPI.Domain.Dtos;
using NotesAPI.Services.IServices;

namespace NotesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("patient/{id}")]
        public async Task<IActionResult> GetAllPatientNotes(Guid id)
        {
            var result = await _noteService.GetAllPatientNotesDTO(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteById(Guid id)
        {
            var result = await _noteService.GetNoteDTOById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteDTO note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _noteService.AddNote(note);
            return result.IsSuccess ? Ok("Note créée avec succès.") : BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(Guid id, [FromBody] UpdateNoteDTO note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _noteService.UpdateNote(id, note);
            return result.IsSuccess ? Ok("Note mise à jour avec succès.") : NotFound(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            var result = await _noteService.DeleteNote(id);
            return result.IsSuccess ? Ok("Note supprimée avec succès.") : NotFound(result.Error);
        }
    }
}
