using ShopProjectDatabase.Database;
using ShopProjectDatabase.Design;
using ShopProjectDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDatabase.Shop
{
    internal class ShopFunctions
    {
        public static void CartOptions(Account account)
        {
            while(true)
            {
                DataContext data = new DataContext();
                try
                {
                    Custom.Line();
                    Say.Green("1", "My cart");
                    Say.Green("2", "Add to cart");
                    Say.Green("3", "Remove from cart");
                    Say.Green("4", "Products");
                    Say.Green("5", "Sorted Products");
                    Say.Red("Any", "Back");
                    Console.Write(" Option: ");
                    int option = int.Parse(Console.ReadLine());
                    if(option == 1)
                    {
                        LogAccountCart(data, account);
                    }
                    else if(option == 2)
                    {
                        AddToAccountCart(data, account);
                    }
                    else if (option == 3)
                    {
                        RemoveFromAccountCart(data, account);
                    }
                    else if (option == 4)
                    {
                        LogAllProducts(data);
                    }
                    else if (option == 5)
                    {
                        LogProductsByCategory(data);
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Say.Red("Error", $"Message: {ex.Message}");
                }
            }
        }
        public static void LogAllProducts(DataContext data)
        {
            if (data.Products.Any())
            {
                Console.WriteLine(" Our products: ");
                data.Products.LogAll();
            }
            else
            {
                Say.Red("Error", "We don't have products yet!");
            }
        }

        public static void LogProductsByCategory(DataContext data)
        {
            if (data.Products.Any())
            {
                Console.WriteLine(" Our categories:");
                data.Categories.LogAll();
                Console.Write(" Category id: ");
                int CategoryId = int.Parse(Console.ReadLine());
                var products = data.Products.Where(x => x.CategoryId == CategoryId).OrderBy(x => x.Price);
                Console.WriteLine($" Our {data.Categories.FindInBaseById(CategoryId).Name} products:");
                products.LogAll();
            }
            else
            {
                Say.Red("Error", "We don't have categories and products yet!");
            }
        }

        public static void LogAccountCart(DataContext data, Account account)
        {
            var AccountCart = data.Carts.FindInBaseByProperty("AccountId", account.Id);
            var AccountsProducts = data.CartModels.Where(x => x.CartId == account.Id);
            if(AccountsProducts.Any())
            {
                Say.Blue($"Products of", $"{account.Username}:", true);
                AccountsProducts.LogAll();
            }
            else
            {
                Say.Red("Error", "Account doesn't have any products in cart!");
            }
        }

        public static void AddToAccountCart(DataContext data, Account account)
        {
            Console.Write(" Product id: ");
            int productId = int.Parse(Console.ReadLine());
            var AccountCart = data.Carts.FindInBaseByProperty("AccountId", account.Id);
            var product = data.Products.FindInBaseById(productId);
            if (product != null)
            {
                Console.Write(" Quantity: ");
                int ProductAmount = int.Parse(Console.ReadLine());
                var contains = data.CartModels.FirstOrDefault(x => x.ProductId == productId && x.CartId == AccountCart.Id);
                if (contains == null && ProductAmount > 0)
                {
                    data.CartModels.AddToBase(data, new CartModel() { CartId = AccountCart.Id, ProductId = product.Id, Amount = ProductAmount });
                    Say.Green("Cart", "Product added to cart successfully!");
                }
                else if (contains != null && ProductAmount > 0)
                {
                    contains.Amount = contains.Amount + ProductAmount;
                    Say.Green("Cart", "You had product in cart (added amount)");
                }
                else
                    Say.Red("Error", "You can't add product with that data!");
            }
            else
                Say.Red("Cart", "Product doesn't found!");
        }

        public static void RemoveFromAccountCart(DataContext data, Account account)
        {
            Console.Write(" Cart item id: ");
            int EnteredId = int.Parse(Console.ReadLine());
            var AccountCart = data.Carts.FindInBaseByProperty("AccountId", account.Id);
            var CartModel = data.CartModels.FindInBaseById(EnteredId);
            if (CartModel != null && CartModel.CartId == AccountCart.Id)
            {
                if (data.CartModels.RemoveFromBaseById(data, CartModel.Id))
                    Say.Green("Cart", "Product removed from cart successfully!");
                else
                    Say.Red("Cart", "Unknown error try again leater!");
            }
            else
            {
                Say.Red("Cart", "Enter correct cart item id please!");
            }
        }
    }
}
