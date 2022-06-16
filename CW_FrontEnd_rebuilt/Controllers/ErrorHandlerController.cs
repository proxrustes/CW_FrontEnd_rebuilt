using Microsoft.AspNetCore.Mvc;

namespace CW_FrontEnd_rebuilt.Controllers
{
    public class ErrorHandlerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
