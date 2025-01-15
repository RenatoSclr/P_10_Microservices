using Frontend.Services.Interface;
using Frontend.ViewModel.NotesViewModel;
using Microsoft.AspNetCore.Mvc;
using CSharpFunctionalExtensions;
using Frontend.ViewModel;

namespace Frontend.Controllers
{
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        public IActionResult Details(Guid patientId)
        {
            var model = new PatientDetailsViewModel
            {
                PatientId = patientId,
                Notes = new List<NoteSummary>() 
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(CreateNoteViewModel model)
        {
            var token = Request.Cookies["auth_token"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await _noteService.CreateNote(model, token);

            if (result.IsFailure)
            {
                TempData["Message"] = result.Error;
            }
            else
            {
                TempData["Message"] = "Note ajoutée avec succès!";
            }

            return RedirectToAction("Details", "Home", new {id = model.PatientId});
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNote(Guid noteId, Guid patientId)
        {
            var token = Request.Cookies["auth_token"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await _noteService.DeleteNote(noteId, token);

            if (result.IsFailure)
            {
                TempData["Message"] = result.Error;
            }
            else
            {
                TempData["Message"] = "Note supprimée avec succès!";
            }

            return RedirectToAction("Details", "Home", new { id = patientId });
        }
    }
}
