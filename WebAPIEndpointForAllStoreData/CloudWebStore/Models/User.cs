using System;
using System.ComponentModel.DataAnnotations;

namespace CloudWebStore.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Password { get; set; }
    }
}
