using ShopProjectDatabase.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDatabase.Models
{
    internal class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public Cart Cart { get; set; }
        public Roles Role { get; set; }

        public override string ToString()
        {
            return $" [Id]: {Id} - [Username]: {Username} - [Age]: {Age} - [Password]: Hashed - [Role]: {Role}";
        }
    }
}
