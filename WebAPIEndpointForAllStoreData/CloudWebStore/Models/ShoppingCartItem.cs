using System;
using System.ComponentModel.DataAnnotations;

namespace CloudWebStore.Models
{
    public class ShoppingCartItem
    {
        public int ID { get; set; }
        public string ProductName { get; set; }

        public string UserId { get; set; }

    }
}
