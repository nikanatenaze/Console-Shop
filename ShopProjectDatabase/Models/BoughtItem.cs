using ShopProjectDatabase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDatabase.Models
{
    internal class BoughtItem
    {
        public int Id { get; set; }
        public int BoughtId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Bought Bought { get; set; }
        public Product Product { get; set; }

        public override string ToString()
        {
            return $" [Id]: {Id} - [Product]: {ProductId} - [Amount]: {Quantity} - [Category]: {Product?.Category}";
        }
        public Product FindProduct(DataContext data)
        {
            return data.Products.FindInBaseById(this.ProductId);
        }
    }
}
