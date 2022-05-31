using Microsoft.AspNetCore.Mvc;

namespace CW_FrontEnd_rebuilt.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult BrowseImages()
        {
            return View();
        }
    }
}
