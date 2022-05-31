using CW_FrontEnd_rebuilt.ApiManager;
using CW_FrontEnd_rebuilt.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CW_FrontEnd_rebuilt.Controllers
{
    public class UserController : Controller
    {
        private readonly IApiController<User> controller;
        public UserController(IApiController<User> _controller)
        {
            controller = _controller;
        }
        [HttpGet("browse")]
        public IActionResult BrowseAll()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                List<User> users = controller.GetAll();
                return View("BrowseUsers", users);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet("edit")]
        public IActionResult Edit()
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
                User model = GetCurrent(int.Parse(HttpContext.Session.GetString("Id")));
                return View("EditProfile", model);
            }
            return RedirectToAction("Index", "Home");
        }
#nullable enable
        public User? GetCurrent(int id)
        {
            return controller.Get(id);
        }
        [HttpPost("reg")]
        public IActionResult Register([FromForm] User user)
        {
            if (user != null)
            {
                user.role = "user";
                controller.Add(user);
            }
            return NotFound("Registration Succesful"); ;
        }

        [HttpPost("log")]
        public IActionResult Login([FromForm] UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return View(userLogin);
            }

            var user = Authenticate(userLogin);
            if (user != null)
            {
                HttpContext.Session.SetString("Id", user.userId.ToString());
                HttpContext.Session.SetString("Name", user.userName);
                HttpContext.Session.SetString("Role", user.role);
                return NotFound("User found");

            }
            return NotFound("User not found");
        }

        [HttpGet("logOut")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return NotFound("LogOut Succesful");
        }
        private User Authenticate(UserLogin userLogin)
        {
            List<User> getAll = controller.GetAll();
#nullable enable
            User? currentUser = getAll.FirstOrDefault(x => x.userName == userLogin.username && x.password == userLogin.password);


            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
    }
}
