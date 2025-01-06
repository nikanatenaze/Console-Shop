using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDatabase.Models
{
    internal class Cart
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
