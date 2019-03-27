using System;

namespace a5.Models
{
    public class PaymentInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string CardNum { get; set; }
        public string PassWord { get; set; }
    }
}