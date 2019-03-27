using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using a5.Models;
using CloudWebStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace a5.Controllers
{
    public class RegisterController : Controller
    {
        UserClient userClient = new UserClient("https://webstoreapp.azurewebsites.net");
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("ID, UserName,ReleaseDate, Password")] User user)
        {
            User tempUser = user;
            var users = await userClient.GetUsersAsync();
            int temp = 0;
            foreach (User u in users)
            {
                if (u.ID > temp)
                    temp = u.ID;
            }
            tempUser.ID = temp + 1;
            //user.ReleaseDate = DateTime.Now;
            System.Net.HttpStatusCode res = userClient.AddUser(user);
            WriteStatusCodeResult(res);
            Console.WriteLine("this is test point: " + res + "this is UserName: " + tempUser.UserName + "this is pssw: " + tempUser.Password);

            if (res == System.Net.HttpStatusCode.Created)
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "login" }));

            }
            else
                return RedirectToAction("Index");

        }

        private void WriteStatusCodeResult(HttpStatusCode statusCode)
        {
            if (statusCode == System.Net.HttpStatusCode.Created)
            {
                Console.WriteLine("Opreation Succeeded - status code {0}", statusCode);
            }
            else
            {
                Console.WriteLine("Opreation Failed - status code {0}", statusCode);
            }
            Console.WriteLine("");
        }
    }
}
