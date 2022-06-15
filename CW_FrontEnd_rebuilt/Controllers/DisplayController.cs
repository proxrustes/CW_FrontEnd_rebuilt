using Microsoft.AspNetCore.Mvc;

namespace CW_FrontEnd_rebuilt.Models
{
    public class DisplayController : Controller
    {
        public IActionResult DisplayMessage(string model)
        {
            return View(model);
        }
    }
}
