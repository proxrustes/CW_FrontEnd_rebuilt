using CW_FrontEnd_rebuilt.ApiManager;
using CW_FrontEnd_rebuilt.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CW_FrontEnd_rebuilt.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IApiController<User> controller;
        public LoginController(IApiController<User> _controller)
        {
            controller = _controller;
        }


        [HttpGet("rd")]
        public IActionResult Redirection()
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
                User model = GetCurrent(int.Parse(HttpContext.Session.GetString("Id")));
                return View("ProfilePage", model);
            }
            return View("Index", "Login");

        }
        [HttpGet("index")]
        public IActionResult Index()
        {
            UserLogin userLogin = new UserLogin();
            return View(userLogin);
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            UserLogin userLogin = new UserLogin();
            return View(userLogin);
        }
        [HttpGet("register")]
        public IActionResult Register()
        {
            User User = new User();
            return View(User);
        }
#nullable enable
        public User? GetCurrent(int id)
        {
            return null;
        }
    }
}
