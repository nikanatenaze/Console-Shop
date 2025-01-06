using ShopProjectDatabase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDatabase.Models
{
    class CartModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int Amount { get; set; }
        public Product Product { get; set; }
        public Cart Cart { get; set; }

        public override string ToString()
        {
            return $" [Id]: {Id} - [Product]: {ProductId} - [Amount]: {Amount} - [Category]: {Product?.Category}";
        }

        public Product FindProduct(DataContext data)
        {
            return data.Products.FindInBaseById(this.ProductId);
        }
    }
}
