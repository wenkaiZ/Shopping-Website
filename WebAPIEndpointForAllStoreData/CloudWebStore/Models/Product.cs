using System;
using System.ComponentModel.DataAnnotations;

namespace CloudWebStore.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
    }
}
