using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace a5.Models
{
    public class CartDB
    {
        public static UserClient userClient = new UserClient("https://webstoreapp.azurewebsites.net");

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await userClient.GetProducts();
            return products;
        }

    }
}
