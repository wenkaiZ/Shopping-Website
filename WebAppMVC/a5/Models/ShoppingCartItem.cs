using System;
using System.ComponentModel.DataAnnotations;

namespace a5.Models
{
    public class ShoppingCartItem
    {
        public int ID { get; set; }
        public string ProductName { get; set; }

        public string UserId { get; set; }

    }
}
