using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDatabase.Design
{
    public class Say
    {
        public static void Red(string prefix, string message, bool space = false)
        {
            if (space)
            {
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(prefix);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($" {message}");
            }
            else
            {
                Console.Write(" [");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(prefix);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"] {message}");
            }
        }
        public static void Green(string prefix, string message, bool space = false)
        {
            if (space)
            {
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(prefix);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($" {message}");
            }
            else
            {
                Console.Write(" [");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(prefix);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"] {message}");
            };
        }
        public static void Blue(string prefix, string message, bool space = false)
        {
            if (space)
            {
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(prefix);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($" {message}");
            }
            else
            {
                Console.Write(" [");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(prefix);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"] {message}");
            }
        }
        public static void Yellow(string prefix, string message, bool space = false)
        {
            if (space)
            {
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(prefix);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($" {message}");
            }
            else
            {
                Console.Write(" [");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(prefix);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"] {message}");
            }
        }
        public static void Magenta(string prefix, string message, bool space = false)
        {
            if (space)
            {
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(prefix);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($" {message}");
            }
            else
            {
                Console.Write(" [");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(prefix);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"] {message}");
            }
        }
        public static void Cyan(string prefix, string message, bool space = true)
        {
            if (space)
            {
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(prefix);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($" {message}");
            }
            else
            {
                Console.Write(" [");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(prefix);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"] {message}");
            }
        }
        public static void Animate(string text, int speed)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(speed);
            }
            Console.WriteLine();
        }
    }
}
