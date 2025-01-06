using ShopProjectDatabase.Database;
using ShopProjectDatabase.Design;
using ShopProjectDatabase.Models;
using ShopProjectDatabase.Shop;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShopProjectDatabase.Menu
{
    internal class RoleMenus
    {
        public static object GuestMenu(DataContext data)
        {
            Account account = null;
            Custom.Line();
            Console.WriteLine(" Your options: ");
            Say.Green("1", "Login");
            Say.Green("2", "Register");
            Say.Green("3", "Products");
            Say.Green("4", "Sorted products");
            Say.Red("Any", "Exit");
            Console.Write(" Option: ");
            int option = int.Parse(Console.ReadLine());
            if (option == 1)
            {
                account = AccountFunctions.Login(data);
            }
            else if (option == 2)
            {
                AccountFunctions.Register(data);
            }
            else if (option == 3)
            {
                ShopFunctions.LogAllProducts(data);
            }
            else if (option == 4)
            {
                ShopFunctions.LogProductsByCategory(data);
            }
            else
            {
                return "exit";
            }
            return account;
        }

        public static object UserMenu(DataContext data, Account account)
        {
            var acc = account;
            if(acc.Role == Roles.User)
            {
                Custom.Line();
                Say.Yellow("Logined with:", $"{account.Username} (Role: {account.Role})", true);
                Say.Green("1", "Cart options");
                Say.Green("2", "Account options");
                Say.Green("3", "Bought options");
                Say.Green("4", "Products");
                Say.Green("5", "Sorted products");
                Say.Red("Any", "Exit");
                Console.Write(" Option: ");
                int option = int.Parse(Console.ReadLine());
                if (option == 1)
                {
                    ShopFunctions.CartOptions(account);
                }
                else if (option == 2)
                {
                    acc = AccountFunctions.AccountOptions(data, acc);
                }
                else if (option == 3)
                {
                    BoughtFunctions.BoughtsOption(account);
                }
                else if (option == 4)
                {
                    ShopFunctions.LogAllProducts(data);
                }
                else if (option == 5)
                {
                    ShopFunctions.LogProductsByCategory(data);
                }
                else
                {
                    return "exit";
                }
                return acc;
            }
            else
            {
                return "exit";
            }
        }

        public static object MenegerMenu(DataContext data, Account account)
        {
            var acc = account;
            if(acc.Role == Roles.Meneger)
            {
                Custom.Line();
                Say.Green("1", "Category Options");
                Say.Green("2", "Products Menu");
                Say.Green("3", "Account Options");
                Say.Red("Any", "Exit");
                Console.Write(" Option: ");
                int option = int.Parse(Console.ReadLine());
                if (option == 1)
                {
                    RolesFunctions.CategoryMenu();
                }
                else if (option == 2)
                {
                    RolesFunctions.ProductsMenu();
                }
                else if (option == 3)
                {
                    acc = AccountFunctions.AccountOptions(data, acc);
                }
                else
                {
                    return "exit";
                }
                return acc;
            }
            else
            {
                return "exit";
            }
        }
        public static object AdminMenu(DataContext data, Account account)
        {
            var acc = account;
            if(acc.Role == Roles.Admin)
            {
                Custom.Line();
                Say.Green("1", "Open Menus");
                Say.Green("2", "User Options");
                Say.Green("3", "Clear bougths");
                Say.Green("4", "Account options");
                Say.Red("Any", "Exit");
                Console.Write(" Option: ");
                int option = int.Parse(Console.ReadLine());
                if(option == 1)
                {
                    Say.Red("System", "This founction is not avalable at this moment!");
                }
                else if (option == 2)
                {
                    RolesFunctions.AccountMenu(account);
                }
                else if (option == 3)
                {
                    BoughtFunctions.ClearBoughts(data);
                }
                else if (option == 4)
                {
                    acc = AccountFunctions.AccountOptions(data, account);
                }
                else
                {
                    return "exit";
                }
            }
            return acc;
        }
    }
}
