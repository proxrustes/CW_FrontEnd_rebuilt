using CW_FrontEnd_rebuilt.ApiManager.general;
using Microsoft.AspNetCore.Http;
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

        //[HttpGet("browse_m")]
        //public IActionResult BrowseImages()
        //{
        //    return View();
        //}
        //[HttpGet("browse_m/{type}/{category}")]
        //public IActionResult BrowseImages(string type, string category)
        //{
        //    List<string> model = controller.getImageByCategoryMany(type, category);

        //    return View(model);
        //}

        [HttpGet("browse_s")]
        public IActionResult BrowseImage()
        {
            return View();
        }
        [HttpGet("browse/sfw/{category}")]
        public IActionResult BrowseImageSFW(string category)
        {
            string[] model = controller.getImageByCategory("sfw", category);
            return View(model);
        }
        [HttpGet("browse/nsfw/{category}")]
        public IActionResult BrowseImageNSFW(string category)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index", "Image");
            }
            string[] model = controller.getImageByCategory("nsfw", category);
            return View(model);
        }
    }
}
