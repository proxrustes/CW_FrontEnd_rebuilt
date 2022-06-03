using CW_FrontEnd_rebuilt.ApiManager.general;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CW_FrontEnd_rebuilt.Controllers
{
    [Route("images")]
    [ApiController]
    public class ImageController : Controller
    {
        private ImageApiController controller = new ImageApiController();

        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("browse_m")]
        public IActionResult BrowseImages()
        {
            return View();
        }
        [HttpPost("browse_m/{type}/{category}")]
        public IActionResult BrowseImages(string type, string category)
        {
            List<string> model = controller.getImageByCategoryMany(type, category);
            return View(model);
        }
        [HttpGet("browse_s")]
        public IActionResult BrowseImage()
        {
            return View();
        }
        [HttpPost("browse/{type}/{category}")]
        public IActionResult BrowseImage(string type, string category)
        {
            string[] model = controller.getImageByCategory(type, category);
            return View(model);
        }
    }
}
