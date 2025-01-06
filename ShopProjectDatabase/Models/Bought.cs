using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDatabase.Models
{
    internal class Bought
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime Time { get; set; }
        public Account account { get; set; }

        public override string ToString()
        {
            return $" [Id]: {Id} - [Account Id]: {AccountId} - [Time]: {Time}"; 
        }
    }
}
