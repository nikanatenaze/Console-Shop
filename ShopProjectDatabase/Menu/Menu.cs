using ShopProjectDatabase.Database;
using ShopProjectDatabase.Design;
using ShopProjectDatabase.Models;
using ShopProjectDatabase.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDatabase.Menu
{
    internal class Menu
    {
        public static void Start()
        {
            Background.TurnOn();
            Say.Blue("Made by:", "nikanatenaze", true);
            Say.Animate(" STEP databese project (Finished)", 1);
            Custom.Natenadze();
            Account LogedAccount = null;
            while (true) {
                try
                {
                    DataContext data = new DataContext();
                    if (LogedAccount == null)
                    {
                        LogedAccount = MenuConnector(() => RoleMenus.GuestMenu(data));
                    }
                    else if(LogedAccount.Role == Roles.User) {
                        LogedAccount = MenuConnector(() => RoleMenus.UserMenu(data, LogedAccount));
                    }
                    else if(LogedAccount.Role == Roles.Meneger) {
                        LogedAccount = MenuConnector(() => RoleMenus.MenegerMenu(data, LogedAccount));
                    }
                    else if(LogedAccount.Role == Roles.Admin)
                    {
                        LogedAccount = MenuConnector(() => RoleMenus.AdminMenu(data, LogedAccount));
                    }
                }
                catch (Exception ex)
                {
                    Say.Red("Error", $"Message: {ex.Message}");
                }
            }
        }

        public static Account MenuConnector(Func<object> func)
        {
            var result = func.Invoke();
            if(result is string e && e == "exit")
            {
                Environment.Exit(0);
            }
            if(result is Account a)
            {
                return a;
            }
            return null;
        }
    }
}
