using CW_FrontEnd_rebuilt.ApiManager;
using CW_FrontEnd_rebuilt.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CW_FrontEnd_rebuilt.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IApiController<User> controller;
        public UserController(IApiController<User> _controller)
        {
            controller = _controller;
        }

        [HttpGet("profile/{id}")]
        public IActionResult ProfilePage(int id)
        {
            User model = GetCurrent(id);
            return View("ProfilePage", model);
        }

        [HttpGet("browse")]
        public IActionResult BrowseAll()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {

                List<User> users = controller.GetAll();
                User[] users_array = users.ToArray();
                return View("BrowseUsers", users_array);
            }
            return View("DisplayMessage", "failed to browse users. please log in");
        }
        [HttpGet("edit")]
        public IActionResult Edit()
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
                User model = GetCurrent(int.Parse(HttpContext.Session.GetString("Id")));
                return View("EditProfile", model);
            }
            return View("DisplayMessage", "failed to edit user profile. please log in");
        }
        [HttpPost("supdate")]
        public IActionResult SEdit([FromForm] User value)
        {
            controller.Update(value);
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
                if(user.userName == "admin" && user.password == "12344321")
                {
                    user.role = "Admin";
                }
                controller.Add(user);
            }
            return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Redirection", "Login");

            }
            return View("DisplayMessage", "user not found");
        }

        [HttpGet("logOut")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return View("DisplayMessage", "logout succesful");
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
