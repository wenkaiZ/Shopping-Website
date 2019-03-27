using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using a5.Models;
using CloudWebStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace a5.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger _logger;
        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        UserClient userClient = CartDB.userClient;

        // GET: /<controller>/
        public IActionResult Index()
        {
            UserClient.loginStatus = false;
            UserClient.username = "";
            return View();
        }
        //[Route("[controller]/Index")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("UserName, Password")] User user)
        {
            if (ModelState.IsValid)
            {

                var users = await userClient.GetUsersAsync();
                foreach (User u in users)
                {
                    if (u.UserName.Equals(user.UserName) && u.Password.Equals(user.Password))
                    {
                        UserClient.loginStatus = true;
                        UserClient.username = user.UserName;
                        _logger.LogInformation("User {username} login at {time}",user.UserName,DateTime.Now);
                        return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "shop" }));
                    }
                }
                return RedirectToAction(nameof(Index));

            }
            return View();
        }
    }
}
