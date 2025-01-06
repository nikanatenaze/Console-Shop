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
    internal class AccountFunctions
    {
        public static void Register(DataContext data)
        {
            Console.Write(" Username: ");
            string Username = Console.ReadLine();
            Console.Write(" Age: ");
            int Age = int.Parse(Console.ReadLine());
            Console.Write(" Password: ");
            string Password = Console.ReadLine();
            var FoundAccount = data.Accounts.FindInBaseByProperty("Username", Username);
            if (FoundAccount != null)
            {
                Say.Red("Error", "Account with that name already exists!");
            }
            else
            {
                string HashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
                Account account1 = new Account() { Username = Username, Role = Roles.User, Password = HashedPassword, Age = Age };
                data.Accounts.AddToBase(data, account1);
                var Account = data.Accounts.FindInBaseByProperty("Username", account1.Username);
                Cart UserCart = new Cart() { AccountId = Account.Id };
                data.Carts.AddToBase(data, UserCart);
                Say.Green("Auth", "Account created successfully");
            }
        }

        public static Account Login(DataContext data)
        {
            if (data.Accounts.Count() == 0)
            {
                Say.Red("Auth", "We don't have accounts yet!");
                return null;
            }
            Console.Write(" Name: ");
            string Name = Console.ReadLine();
            Console.Write(" Password: ");
            string Password = Console.ReadLine();
            Account account = data.Accounts.FindInBaseByProperty("Username", Name);
            if (account != null)
            {
                bool valid = BCrypt.Net.BCrypt.Verify(Password, account.Password);
                if (valid)
                {
                    Say.Green("Auth", "Loged in successfully");
                    return account;
                }
                else
                {
                    Say.Red("Auth", "incorrect password of account");
                    return null;
                }
            }
            Say.Red("Auth", "Account doesn't found, incorrect data");
            return null;
        }

        public static Account AccountOptions(DataContext data, Account account)
        {
            var acc = data.Accounts.FindInBaseById(account.Id);
            while(true)
            {
                try
                {
                    Custom.Line();
                    Say.Green("1", "Information");
                    Say.Green("2", "Promo code");
                    Say.Green("3", "Rename");
                    Say.Green("4", "Change password");
                    Say.Green("5", "Delete Account");
                    Say.Green("6", "Logout");
                    Say.Red("Any", "Back");
                    Console.Write(" Option: ");
                    int option = int.Parse(Console.ReadLine());
                    if (option == 1)
                    {
                        Console.WriteLine(" Information: ");
                        Console.WriteLine(acc);
                    }
                    else if (option == 2)
                    {
                        Console.Write(" Code: ");
                        string code = Console.ReadLine();
                        if (code == "Admin")
                        {
                            if (acc.Role != Roles.Admin)
                            {
                                acc.Role = Roles.Admin;
                                data.SaveChanges();
                                Say.Green("System", "Code actived successfully!");
                            }
                            else
                            {
                                Say.Red("Error", "This account already is admin!");
                            }
                        }
                        else if (code == "Meneger")
                        {
                            if (acc.Role != Roles.Meneger)
                            {
                                acc.Role = Roles.Meneger;
                                data.SaveChanges();
                                Say.Green("System", "Code actived successfully!");
                            }
                            else
                            {
                                Say.Red("Error", "This account already is Meneger!");
                            }
                        }
                        else
                        {
                            Say.Red("System", "Inccorect code!");
                        }
                    }
                    else if (option == 3)
                    {
                        Console.Write(" New username: ");
                        string newUsername = Console.ReadLine();
                        var valid = data.Accounts.FindInBaseByProperty("Username", newUsername);
                        if (valid == null)
                        {
                            acc.Username = newUsername;
                            data.SaveChanges();
                            Say.Green("System", "Renamed account successfully!");
                        }
                        else
                        {
                            Say.Red("System", "Account with same username already exists!");
                        }
                    }
                    else if (option == 4)
                    {
                        Console.Write(" New password: ");
                        string newPassword = Console.ReadLine();
                        acc.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                        data.SaveChanges();
                        Say.Green("Auth", "Password changed successfully");
                    }
                    else if (option == 5)
                    {
                        if (data.Accounts.RemoveFromBaseById(data, acc.Id))
                        {
                            Say.Green("System", "Account removed successfully");
                            return null;
                        }
                        else
                        {
                            Say.Red("System", "Can't delete that account!");
                            return acc;
                        }
                    }
                    else if (option == 6)
                    {
                        return null;
                    }
                    else
                    {
                        return data.Accounts.FindInBaseById(acc.Id);
                    }
                }
                catch (Exception ex)
                {
                    Say.Red("Error", $"Message: {ex.Message}");
                }
            }
        }
    }
}
