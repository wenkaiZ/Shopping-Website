using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CloudWebStore.Models
{
    public class MvcProductContext : DbContext
    {
        public MvcProductContext(DbContextOptions<MvcProductContext> options)
            : base(options)
        {
        }

        public DbSet<CloudWebStore.Models.Product> Product { get; set; }
        public DbSet<CloudWebStore.Models.ShoppingCartItem> ShoppingCartItem { get; set; }
        public DbSet<CloudWebStore.Models.User> User { get; set; }
        public DbSet<CloudWebStore.Models.PaymentInfo> PaymentInfo { get; set; }
    }
}
