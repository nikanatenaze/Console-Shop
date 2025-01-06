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
    internal class BoughtFunctions
    {
        public static void BoughtsOption(Account account)
        {
            while(true) {
                DataContext data = new DataContext();
                try
                {
                    Custom.Line();
                    Say.Green("1", "Boughts list");
                    Say.Green("2", "Bought items");
                    Say.Green("3", "Pay for cart");
                    Say.Red("Any", "Back");
                    Console.Write(" Option: ");
                    int option = int.Parse(Console.ReadLine());
                    if(option == 1) {
                        BoughtList(data, account);
                    }
                    else if(option == 2)
                    {
                        BoughtItems(data, account);
                    }
                    else if(option == 3)
                    {
                        AddBoughtWithMessages(data, account);
                    }
                }
                catch (Exception ex)
                {
                    Say.Red("Error", $"Message: {ex.Message}");
                }
            }
        }

        public static bool BoughtList(DataContext data, Account account)
        {
            if(data.Boughts.Any())
            {
                var AccountBoughts = data.Boughts.Where(x => x.AccountId == account.Id);
                if(AccountBoughts.Any())
                {
                    Console.WriteLine(" Your boughts: ");
                    AccountBoughts.LogAll();
                    return true;
                }
                else
                {
                    Say.Red("Error", "You don't have boughts");
                    return false;
                }
            }
            else
            {
                Say.Red("Error", "Pardon sir you don't have boughts!");
                return false;
            }
        }

        public static void BoughtItems(DataContext data, Account account)
        {
            if(data.Boughts.Any() && data.BoughtItems.Any())
            {
                if(BoughtList(data, account)) {
                    int BoughtId = int.Parse(Console.ReadLine());
                    Console.Write(" Bought Id: ");
                    var Bought = data.Boughts.FindInBaseById(BoughtId);
                    if (Bought != null && Bought.AccountId == account.Id)
                    {
                        var boughtItems = data.BoughtItems.Where(x => x.BoughtId == Bought.Id);
                        if (boughtItems != null)
                        {
                            Say.Blue("Bought items of that bought of:", $"{account.Username}:", true);
                            boughtItems.LogAll();
                        }
                    }
                }
            }
        }

        public static bool AddBoughtFunction(DataContext data, Account account)
        {
            var AccountCart = data.Carts.FindInBaseByProperty("AccountId", account.Id);
            if(AccountCart != null)
            {
                var AccountCartItems = data.CartModels.Where(x => x.CartId == AccountCart.Id);
                if(AccountCartItems.Any()) {
                    data.Boughts.AddToBase(data, new Bought() { AccountId = account.Id, Time = DateTime.Now });
                    var AccountBought = data.Boughts.FindInBaseByProperty("AccountId", account.Id);
                    var NewBoughtItems = new List<BoughtItem>();
                    foreach (var i in AccountCartItems)
                    {
                        NewBoughtItems.Add(new BoughtItem() { BoughtId = AccountBought.Id, ProductId = i.ProductId, Quantity = i.Amount });
                    }
                    data.BoughtItems.AddToBase(data, NewBoughtItems);
                    data.CartModels.RemoveByObject(data, AccountCartItems);
                    return true;
                }
                return false;
            }
            return false;
        }

        public static void AddBoughtWithMessages(DataContext data, Account account)
        {
            bool result = AddBoughtFunction(data, account);
            if(result)
            {
                Say.Green("Boughts", "Items in your cart bought successfully!");
            }
            else
            {
                Say.Red("Error with buying, check reasons below", "", true);
                Say.Yellow(" - Check your cart", "", true);
                Say.Yellow(" - Check for valid data", "", true);
            }
        }

        public static void ClearBoughts(DataContext data)
        {
            data.BoughtItems.RemoveByObject(data, data.BoughtItems);
            data.Boughts.RemoveByObject(data, data.Boughts);
            Say.Green("Boughts", "Data cleared successfully!");
        }
    }
}
