using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class NoteController : Controller
    {
        public IActionResult CreateNote()
        {
            return View();
        }
    }
}
