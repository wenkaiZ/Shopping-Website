using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CloudWebStore.Models;
using Newtonsoft.Json;

namespace a5.Models
{
    public class UserClient
    {
        public static string username = "";
        public static bool loginStatus = false;
        string _hostUri;
        public UserClient(string hostUri)
        {
            _hostUri = hostUri;
        }
        public HttpClient CreateClient()
        {
            var client = new HttpClient();
            //Passing service base url    
            client.BaseAddress = new Uri(_hostUri);

            client.DefaultRequestHeaders.Clear();
            //Define request data format    
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
        public HttpClient CreateActionClient(string action)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(new Uri(_hostUri), "login/" + action);
            return client;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response;
                Console.WriteLine(client.BaseAddress);

                response = await client.GetAsync("api/users");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list    
                    return JsonConvert.DeserializeObject<List<User>>(result);
                }
                else
                {
                    return null;
                }
            }
        }

        public System.Net.HttpStatusCode AddUser(User user)
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response = null;
                try
                {
                    Console.WriteLine("this is try block: ");
                    //response = client.PostAsJsonAsync(client.BaseAddress, company).Result;
                    var output = JsonConvert.SerializeObject(user);
                    Console.WriteLine("this is try block: output: " + output);

                    HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");
                    Console.WriteLine("this is try block: contentpost " + contentPost + "user content: " + user.UserName);

                    client.BaseAddress = new Uri(new Uri(_hostUri), "api/Users/Create");
                    response = client.PostAsync(client.BaseAddress, contentPost).Result;
                    Console.WriteLine("this is try block: respond: " + response.StatusCode + "   " + client.BaseAddress + "    " + contentPost.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("this is catch block: " + ex.Message);
                }
                return response.StatusCode;
            }
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response;
                Console.WriteLine(client.BaseAddress);
                //set the route
                response = await client.GetAsync("products/Get");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<List<Product>>(result);

                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems()
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response;
                Console.WriteLine(client.BaseAddress);
                //set the route
                response = await client.GetAsync("shoppingcartitem/Get");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(result);

                }
                else
                {
                    return null;
                }
            }
        }



        public System.Net.HttpStatusCode AddShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response = null;
                try
                {
                    Console.WriteLine("this is try block: ");
                    //response = client.PostAsJsonAsync(client.BaseAddress, company).Result;
                    var output = JsonConvert.SerializeObject(shoppingCartItem);
                    Console.WriteLine("this is try block: output: " + output);

                    HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(shoppingCartItem), System.Text.Encoding.UTF8, "application/json");
                    Console.WriteLine("this is try block: contentpost " + contentPost + "user content: " + shoppingCartItem.UserId);

                    client.BaseAddress = new Uri(new Uri(_hostUri), "ShoppingCartItem/Create");
                    response = client.PostAsync(client.BaseAddress, contentPost).Result;
                    Console.WriteLine("this is try block: respond: " + response.StatusCode + "   " + client.BaseAddress + "    " + contentPost.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("this is catch block: " + ex.Message);
                }
                return response.StatusCode;
            }
        }

        public System.Net.HttpStatusCode DeleteShoppingCartItem(int ID)
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response = null;
                try
                {   
                    Console.WriteLine("this is try block: ");
                    //response = client.PostAsJsonAsync(client.BaseAddress, company).Result;
                    var output = JsonConvert.SerializeObject(ID);
                    Console.WriteLine("this is try block: output: " + output);

                    HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(ID), System.Text.Encoding.UTF8, "application/json");
                    Console.WriteLine("this is try block: contentpost " + contentPost + "user content: " + ID);

                    //user ID in route to find the corrosponding item
                    client.BaseAddress = new Uri(new Uri(_hostUri), "ShoppingCartItem/DeleteOne/"+ID);
                    response = client.PostAsync(client.BaseAddress, contentPost).Result;
                    Console.WriteLine("this is try block: respond: " + response.StatusCode + "   " + client.BaseAddress + "    " + contentPost.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("this is catch block: " + ex.Message);
                }
                return response.StatusCode;
            }
        }

        public System.Net.HttpStatusCode DeleteShoppingCartItem(String username)
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response = null;
                try
                {
                    Console.WriteLine("this is try block: ");
                    //response = client.PostAsJsonAsync(client.BaseAddress, company).Result;
                    var output = JsonConvert.SerializeObject(username);
                    Console.WriteLine("this is try block: output: " + output);

                    HttpContent contentPost = new StringContent(output, System.Text.Encoding.UTF8, "application/json");
                    Console.WriteLine("this is try block: contentpost " + contentPost + "user content: " + username);

                    //user ID in route to find the corrosponding item
                    client.BaseAddress = new Uri(new Uri(_hostUri), "ShoppingCartItem/Delete?username="+username);
                    response = client.PostAsync(client.BaseAddress, contentPost).Result;
                    Console.WriteLine("this is try block: respond: " + response.StatusCode + "   " + client.BaseAddress + "    " + contentPost.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("this is catch block: " + ex.Message);
                }
                return response.StatusCode;
            }
        }
        public async Task<IEnumerable<PaymentInfo>> GetPaymentInfosAsync()
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response;
                Console.WriteLine(client.BaseAddress);
                //set the route
                response = await client.GetAsync("paymentinfo/Get");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<List<PaymentInfo>>(result);

                }
                else
                {
                    return null;
                }
            }
        }

        public System.Net.HttpStatusCode AddPaymentInfo(PaymentInfo paymentInfo)
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response = null;
                try
                {
                    Console.WriteLine("this is try block: ");
                    //response = client.PostAsJsonAsync(client.BaseAddress, company).Result;
                    var output = JsonConvert.SerializeObject(paymentInfo);
                    Console.WriteLine("this is try block: output: " + output);

                    HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(paymentInfo), System.Text.Encoding.UTF8, "application/json");
                    Console.WriteLine("this is try block: contentpost " + contentPost + "paymentInfo content: " + paymentInfo.UserName);

                    client.BaseAddress = new Uri(new Uri(_hostUri), "paymentinfo/Create");
                    response = client.PostAsync(client.BaseAddress, contentPost).Result;
                    Console.WriteLine("this is try block: respond: " + response.StatusCode + "   " + client.BaseAddress + "    " + contentPost.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("this is catch block: " + ex.Message);
                }
                return response.StatusCode;
            }
        }


    }
}