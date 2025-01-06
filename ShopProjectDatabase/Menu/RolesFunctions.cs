using Microsoft.EntityFrameworkCore;
using ShopProjectDatabase.Database;
using ShopProjectDatabase.Design;
using ShopProjectDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDatabase.Menu
{
    internal class RolesFunctions
    {
        public static void CategoryMenu()
        {
            while(true)
            {
                DataContext data = new DataContext();
                try
                {
                    Custom.Line();
                    Say.Green("1", "Categories");
                    Say.Green("2", "Add category");
                    Say.Green("3", "Remove category");
                    Say.Red("Any", "Back");
                    Console.Write(" Option: ");
                    int option = int.Parse(Console.ReadLine());
                    if (option == 1)
                    {
                        data.Categories.LogAll();
                    }
                    else if (option == 2)
                    {
                        Console.Write(" New category name: ");
                        string NewCategoryName = Console.ReadLine();
                        var Exists = data.Categories.FirstOrDefault(x => x.Name == NewCategoryName);
                        if (Exists == null)
                        {
                            data.Categories.AddToBase(data, new Category() { Name = NewCategoryName });
                            Say.Green("System", "Category added successfully!");
                        }
                        else
                        {
                            Say.Red("Error", "Category with same name already exists!");
                        }
                    }
                    else if (option == 3)
                    {
                        if(data.Categories.Any())
                        {
                            Console.Write(" Category name: ");
                            string CategoryName = Console.ReadLine();
                            var category = data.Categories.FindInBaseByProperty("Name", CategoryName);
                            if (category != null)
                            {
                                var CartModelsProducts = data.CartModels.AsEnumerable().Where(x => x.FindProduct(data).CategoryId == category.Id);
                                var AllProducts = data.Products.Where(x => x.CategoryId == category.Id);
                                Custom.Line();
                                Say.Yellow("Are you sure u want remove category?", "", true);
                                Say.Yellow("All category products and items in account carts will be removed", "", true);
                                Say.Yellow($"Products: {AllProducts.Count()} - Items from carts: {CartModelsProducts.Count()}", "", true);
                                bool answer = Custom.YesNo();
                                if (answer)
                                {
                                    data.CartModels.RemoveByObject(data, CartModelsProducts);
                                    data.Products.RemoveByObject(data, AllProducts);
                                    data.Categories.RemoveByObject(data, category);
                                    Say.Green("System", "All removed successfully!");
                                }
                                else
                                {
                                    Say.Red("System", "Returning back");
                                }
                            }
                            else
                            {
                                Say.Red("Error", "Category doesn't found!");
                            }
                        }
                        else
                        {
                            Say.Red("Error", "We don't have categories to delete!");
                        }
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
        public static void ProductsMenu()
        {
            while(true)
            {
                DataContext data = new DataContext();
                try
                {
                    Custom.Line();
                    Say.Green("1", "All products");
                    Say.Green("2", "Add product");
                    Say.Green("3", "Remove product");
                    Say.Green("4", "Clear products by category");
                    Say.Green("5", "Categories");
                    Say.Red("Any", "Back");
                    Console.Write(" Option: ");
                    int option = int.Parse(Console.ReadLine());
                    if(option == 1)
                    {
                        if (data.Products.Any())
                        {
                            Console.WriteLine(" All products: ");
                            data.Products.LogAll();
                        }
                        else
                            Say.Red("Error", "We don't have products yet!");
                    }
                    else if(option == 2)
                    {
                        if (data.Categories.Any())
                        {
                            Console.Write(" Name: ");
                            string NewProductName = Console.ReadLine();
                            Console.Write(" Price: ");
                            int NewProductPrice = int.Parse(Console.ReadLine());
                            Console.Write(" Category id: ");
                            int NewProductCategoryName = int.Parse(Console.ReadLine());
                            var validProductName = data.Products.FindInBaseByProperty("Name", NewProductName);
                            var validCategory = data.Categories.FindInBaseByProperty("Id", NewProductCategoryName);
                            if (validProductName == null && validCategory != null)
                            {
                                var NewProductObject = new Product() { CategoryId = validCategory.Id, Name = NewProductName, Price = NewProductPrice };
                                data.Products.AddToBase(data, NewProductObject);
                                Say.Green("System", "Product added successfully!");
                            }
                            else if (validCategory == null)
                            {
                                Say.Red("Error", "Category doesn't found!");
                            }
                            else
                            {
                                Say.Red("Error", "Product with same name already exits!");
                            }
                        }
                        else
                        {
                            Say.Red("Error", "We don't have categories!");
                        }
                    }
                    else if(option == 3)
                    {
                        if(data.Products.Any())
                        {
                            Console.Write(" Product Id: ");
                            int ProductId = int.Parse(Console.ReadLine());
                            var result = data.Products.RemoveFromBaseById(data, ProductId);
                            if (result)
                            {
                                Say.Green("System", "Product removed successfully!");
                            }
                            else
                            {
                                Say.Red("System", "We can't find your product!");
                            }
                        }
                        else
                        {
                            Say.Red("Error", "We don't have products yet!");
                        }
                    }
                    else if(option == 4)
                    {
                        if(data.Products.Any())
                        {
                            Console.Write(" Category id: ");
                            int CategoryId = int.Parse(Console.ReadLine());
                            var category = data.Categories.FindInBaseById(CategoryId);
                            if (category != null)
                            {
                                var products = data.Products.Where(x => x.CategoryId == category.Id);
                                var CartModelsList = data.CartModels.AsEnumerable().Where(x => x.FindProduct(data).CategoryId == category.Id);
                                Custom.Line();
                                Say.Yellow("Are you sure u want remove product by category?", "", true);
                                Say.Yellow("Products and items in account carts will be removed", "", true);
                                Say.Yellow($"Products: {products.Count()} - Items from carts: {CartModelsList.Count()}", "", true);
                                bool answer = Custom.YesNo();
                                if(answer)
                                {
                                    data.CartModels.RemoveByObject(data, CartModelsList);
                                    data.Products.RemoveByObject(data, products);
                                    Say.Green("System", "Products by category removed successfully!");
                                }
                                else
                                {
                                    Say.Red("System", "Returning back");
                                }
                            }
                            else
                            {
                                Say.Red("Error", "Category doesn't found!");
                            }
                        }
                        else
                        {
                            Say.Red("Error", "We don't have any products!");
                        }
                    }
                    else if(option == 5)
                    {
                        if(data.Categories.Any())
                        {
                            Console.WriteLine(" Categories: ");
                            data.Categories.LogAll();
                        }
                        else {
                            Say.Red("Error", "We don't have categories!");
                        }
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
        public static void AccountMenu(Account account)
        {
            while(true)
            {
                DataContext data = new DataContext();
                try
                {
                    Custom.Line();
                    Say.Green("1", "Accounts list");
                    Say.Green("2", "Delete account");
                    Say.Green("3", "Create account");
                    Say.Green("4", "Give role");
                    Say.Green("5", "Clear account cart");
                    Say.Red("Any", "Back");
                    Console.Write(" Option: ");
                    int option = int.Parse(Console.ReadLine());
                    if(option == 1)
                    {
                        Console.WriteLine(" All account: ");
                        data.Accounts.LogAll();
                    }
                    else if(option == 2) {
                        Console.Write(" Account Id: ");
                        int DelAccountId = int.Parse(Console.ReadLine());
                        var Acc = data.Accounts.FindInBaseById(DelAccountId);
                        if(Acc != null)
                        {
                            if(Acc.Id != account.Id)
                            {
                                data.Accounts.RemoveByObject(data, Acc);
                                Say.Green("System", "Account removed successfully!");
                            }
                            else
                            {
                                Say.Red("System", "You can't delete own account from here!");
                            }
                        }
                        else
                        {
                            Say.Red("Error", "Enter correct id. account doesn't found!");
                        }
                    }
                    else if(option == 3)
                    {
                        AccountFunctions.Register(data);
                    }
                    else if (option == 4)
                    {
                        Console.Write(" Account Id: ");
                        int AccountId = int.Parse(Console.ReadLine());
                        var Acc = data.Accounts.FindInBaseById(AccountId);
                        if(Acc != null  && Acc.Id != account.Id)
                        {
                            Console.WriteLine(" Roles Id: ");
                            int id = int.Parse(Console.ReadLine());
                            if (id >= 0 && id <= 2)
                            {
                                if((int)Acc.Role != id) { 
                                    Acc.Role = (Roles)id;
                                    Say.Green("System", "Role set successfully!");
                                }
                                else
                                {
                                    Say.Red("System", "This account already is that role!");
                                }
                            }
                            else
                            {
                                Say.Red("Error", "Enter correct role id please!");
                            }
                        }
                        else
                        {
                            Say.Red("Error", "Can't change role of account!");
                        }
                    }
                    else if(option == 5)
                    {
                        Console.Write(" Account Id: ");
                        int AccountId = int.Parse(Console.ReadLine());
                        var Acc = data.Accounts.FindInBaseById(AccountId);
                        if(Acc != null)
                        {
                            var accountCart = data.Carts.FindInBaseByProperty("AccountId", Acc.Id);
                            var accountCartModels = data.CartModels.Where(x => x.CartId == accountCart.Id);
                            data.CartModels.RemoveByObject(data, accountCartModels);
                            Say.Green("System", "Account's cart cleaned successfully!");
                        }
                        else
                        {
                            Say.Red("Error", "Account doesn't found!");
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Say.Red("Error", $"Message: {ex}");
                }
            }
        }
    }
}
