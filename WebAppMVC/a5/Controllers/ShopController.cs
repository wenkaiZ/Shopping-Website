using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using a5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace a5.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger _logger;
        public ShopController(ILogger<ShopController> logger)
        {
            _logger = logger;
        }
        UserClient userClient = CartDB.userClient;
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            if (UserClient.loginStatus)
            {

                var products = await userClient.GetProducts();
                return View(products);
            }

            return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "login" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string ProductName, string UserId)
        {
            ShoppingCartItem newItem = new ShoppingCartItem();
            newItem.ProductName = ProductName;
            newItem.UserId = UserId;
            var allItems = await userClient.GetShoppingCartItems();
            int temp = 0;
            foreach (ShoppingCartItem s in allItems)
            {
                if (s.ID > temp)
                    temp = s.ID;
            }
            newItem.ID = temp + 1;

            System.Net.HttpStatusCode res = userClient.AddShoppingCartItem(newItem);
            _logger.LogInformation("User {username} added {ProductName} to cart at {time}", UserClient.username,ProductName,DateTime.Now);
            Console.WriteLine("this is " + res);
            if (res == System.Net.HttpStatusCode.Created)
            {
                var products = await userClient.GetProducts();
                return View(products);
            }
            return Content(res.ToString());
        }

        public async Task<IActionResult> PaymentInfo()
        {
            if (UserClient.loginStatus)
            {

                var paymentInfos = await userClient.GetPaymentInfosAsync();
                return View(paymentInfos);
            }

            return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "login" }));
        }
        [HttpPost]
        public async Task<IActionResult> PaymentInfo(int Id, string UserName, string CardNum, string PassWord)
        {
            //userClient.DeleteShoppingCartItem(UserClient.username);
            if (UserClient.loginStatus)
            {
                PaymentInfo paymentInfo = new PaymentInfo();
                paymentInfo.UserName = UserName;
                paymentInfo.CardNum = CardNum;
                paymentInfo.PassWord = PassWord;
                var paymentInfos = await userClient.GetPaymentInfosAsync();
                int temp = 0;
                foreach (PaymentInfo p in paymentInfos)
                {
                    if (p.Id > temp)
                        temp = p.Id;
                }
                paymentInfo.Id = temp + 1;
                //add info
                System.Net.HttpStatusCode resAdd = userClient.AddPaymentInfo(paymentInfo);
                //delete shopping items
                System.Net.HttpStatusCode resDelete = userClient.DeleteShoppingCartItem(UserClient.username);
                return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "shop" }));
            }

            return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "login" }));
        }

        public IActionResult Checkout()
        {
            //userClient.DeleteShoppingCartItem(UserClient.username);
            if (UserClient.loginStatus)
            {
                System.Net.HttpStatusCode res = userClient.DeleteShoppingCartItem(UserClient.username);
                return View();
            }

            return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "login" }));
        }
        //this method is used to get all items in cart

        public async Task<IActionResult> Cart()
        {
            ViewData["Total"] = (decimal)0;
            if (UserClient.loginStatus)
            {
                var shoppingCartItems = await userClient.GetShoppingCartItems();
                foreach (var carItem in userClient.GetShoppingCartItems().Result)
                {
                    if (carItem.UserId.Equals(UserClient.username))
                    {
                        foreach (var product in userClient.GetProducts().Result)
                        {
                            if (product.ProductName.Equals(carItem.ProductName))
                            {
                                ViewData["Total"] = (decimal)ViewData["Total"] + product.Price;
                            }
                        }
                    }
                }

                return View(shoppingCartItems);
            }
            return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "login" }));

        }

        //this is used to delete item in cart
        [HttpPost]
        public async Task<IActionResult> Cart(int ID)
        {
            if (UserClient.loginStatus)
            {
                System.Net.HttpStatusCode res = userClient.DeleteShoppingCartItem(ID);
                Console.WriteLine("this is Cart res: " + res);
                ViewData["Total"] = (decimal)0;
                if (UserClient.loginStatus)
                {
                    var shoppingCartItems = await userClient.GetShoppingCartItems();
                    foreach (var carItem in userClient.GetShoppingCartItems().Result)
                    {
                        if (carItem.UserId.Equals(UserClient.username))
                        {
                            foreach (var product in userClient.GetProducts().Result)
                            {
                                if (product.ProductName.Equals(carItem.ProductName))
                                {
                                    ViewData["Total"] = (decimal)ViewData["Total"] + product.Price;
                                }
                            }
                        }
                    }

                    return View(shoppingCartItems);
                }
            }
            return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "login" }));

        }

        public IActionResult Crush()
        {
            throw Exception();
        }

        private Exception Exception()
        {
            throw new NotImplementedException();
        }
    }
}
