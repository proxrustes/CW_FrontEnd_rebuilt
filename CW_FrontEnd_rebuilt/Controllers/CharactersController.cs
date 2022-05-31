using CW_FrontEnd_rebuilt.ApiManager;
using CW_FrontEnd_rebuilt.ObjectModel;
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
            return View(model);
        }
        [HttpGet("add")]
        public IActionResult AddCharacter()
        {
            Character model = new Character();
            return View(model);
        }

        [HttpGet("view/{id}")]
        public IActionResult CharacterProfile(int id)
        {
            Character model = controller.Get(id);
            return View(model);
        }
    }
}
