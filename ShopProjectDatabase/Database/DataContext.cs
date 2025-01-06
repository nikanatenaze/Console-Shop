using Microsoft.EntityFrameworkCore;
using ShopProjectDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDatabase.Database
{
    internal class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartModel> CartModels { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bought> Boughts { get; set; }
        public DbSet<BoughtItem> BoughtItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"MultipleActiveResultSets=True; Data Source=DESKTOP-2BVUIBI;Initial Catalog=ProjectShop2Db;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True");
        }
    }
}
