using System;
using System.Threading;
using System.Windows.Forms;

namespace LowLevelControls.Sample2
{
    static class Program
    {
        static KeyboardHook kbdHook = new KeyboardHook();
        static MouseHook msHook = new MouseHook();

        static void Main(string[] args)
        {
            kbdHook.KeyDownEvent += (sender, vkCode) =>
            {
                Console.WriteLine((Keys)vkCode + " Down");
                return false;
            };
            kbdHook.KeyPressEvent += (sender, vkCode) =>
            {
                Console.WriteLine((Keys)vkCode + " Press");
                return false;
            };
            kbdHook.KeyUpEvent += (sender, vkCode) =>
            {
                Console.WriteLine((Keys)vkCode + " Up");
                return false;
            };

            msHook.MouseDownEvent += (sender, vkCode, x, y, delta) =>
            {
                Console.WriteLine((Keys)vkCode + $" Down on ({x}, {y})");
                return false;
            };
            msHook.MouseUpEvent += (sender, vkCode, x, y, delta) =>
            {
                Console.WriteLine((Keys)vkCode + $" Up on ({x}, {y})");
                return false;
            };
            msHook.MouseMoveEvent += (sender, vkCode, x, y, delta) =>
            {
                Console.WriteLine($"Mouse Move to ({x}, {y})");
                return false;
            };
            msHook.MouseWheelEvent += (sender, vkCode, x, y, delta) =>
            {
                Console.WriteLine($"Mouse Wheel with data {delta} on ({x}, {y})");
                return false;
            };
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            kbdHook.InstallGlobalHook();
            msHook.InstallGlobalHook();
            for (;;)
            {
                Thread.Sleep(1);
                Application.DoEvents();
            }
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            kbdHook.UninstallGlobalKeyboardHook();
            msHook.UninstallGlobalKeyboardHook();
        }
    }
}
