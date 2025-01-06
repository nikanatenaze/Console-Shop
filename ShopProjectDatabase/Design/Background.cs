using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDatabase.Design
{
    internal class Background
    {
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int LWA_ALPHA = 0x2;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetLayeredWindowAttributes(IntPtr hWnd, byte bAlpha, byte dwFlags, byte crKey);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        public static void TurnOn()
        {
            Console.ForegroundColor = ConsoleColor.White;
            IntPtr hWnd = GetConsoleWindow();
            int windowLong = GetWindowLong(hWnd, GWL_EXSTYLE);
            SetWindowLong(hWnd, GWL_EXSTYLE, windowLong | WS_EX_LAYERED);
            byte opacity = 220;
            SetLayeredWindowAttributes(hWnd, 0, opacity, LWA_ALPHA);
        }
    }
}
