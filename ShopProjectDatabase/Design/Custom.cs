using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDatabase.Design
{
    public class Custom
    {
        public static bool YesNo()
        {
            Console.Write(" [Y/N]: ");
            string readline = Console.ReadLine();
            if(readline.ToLower() == "y")
            {
                return true;
            }
            return false;
        }
        public static void Line()
        {
            Say.Animate("---------------------------------------------------------------------->", 1);
        }
        public static void Natenadze()
        {
            Console.WriteLine("  _   _ _      _          ____  _                 \r\n | \\ | (_) ___| | _____  / ___|| |__   ___  _ __  \r\n |  \\| | |/ __| |/ / __| \\___ \\| '_ \\ / _ \\| '_ \\ \r\n | |\\  | | (__|   <\\__ \\  ___) | | | | (_) | |_) |\r\n |_| \\_|_|\\___|_|\\_\\___/ |____/|_| |_|\\___/| .__/ \r\n                                           |_|    ");
        }
    }
}
