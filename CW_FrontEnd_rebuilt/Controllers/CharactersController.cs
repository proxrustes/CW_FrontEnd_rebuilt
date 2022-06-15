using CW_FrontEnd_rebuilt.ApiManager;
using CW_FrontEnd_rebuilt.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CW_FrontEnd_rebuilt.Controllers
{
    [Route("characters")]
    [ApiController]
    public class CharactersController : Controller
    {
        private IApiController<Character> controller;

        public CharactersController(IApiController<Character> _controller)
        {
            controller = _controller;
        }

        [HttpGet("browse")]
        public IActionResult BrowseCharacters()
        {
            List<Character> model = controller.GetAll();
            Character[] model_array = model.ToArray();
            return View(model_array);
        }

        [HttpGet("add")]
        public IActionResult AddCharacter()
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
            Character model = new Character();
            model.userId = int.Parse(HttpContext.Session.GetString("Id"));
            return View(model);
            }
            return RedirectToAction("DisplayMessage", "Display", "failed to add character");
        }

        [HttpGet("update/{id}")]
        public IActionResult EditCharacter(int id)
        {
            if (HttpContext.Session.GetString("Id") != null )
            {
                Character model = controller.Get(id);
                if (model.userId == int.Parse(HttpContext.Session.GetString("Id")))
                {
                return View(model);
                }
            }
            return View("DisplayMessage", "failed to edit character");
        }

        [HttpPost("supdate")]
        public IActionResult SEditCharacter([FromForm] Character value)
        {
           controller.Update(value);
            return RedirectToAction("BrowseCharacters", "Characters");
        }

        [HttpPost("sadd")]
        public IActionResult SAddCharacter([FromForm] Character value)
        {
            controller.Add(value);
            return RedirectToAction("BrowseCharacters", "Characters");
        }

        [HttpGet("view/{id}")]
        public IActionResult CharacterProfile(int id)
        {
            Character model = controller.Get(id);
            return View(model);
        }

        [HttpGet("remove/{id}")]
        public IActionResult Remove(int id)
        {
            if (HttpContext.Session.GetString("Id") != null && int.Parse(HttpContext.Session.GetString("Id")) == id)
            {
                controller.Delete(id);
                return RedirectToAction("Redirection", "Login");
            }
            return RedirectToAction("DisplayMessage", "Display", "failed to delete character. please login");
           
        }
    }
}
