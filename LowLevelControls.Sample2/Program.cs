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
            kbdHook.KeyDownEvent += (sender, vkCode, injected) =>
            {
                Console.WriteLine((Keys)vkCode + " Down" + (injected ? " (Injected)" : ""));
                return false;
            };
            kbdHook.KeyPressEvent += (sender, vkCode, injected) =>
            {
                Console.WriteLine((Keys)vkCode + " Press" + (injected ? " (Injected)" : ""));
                return false;
            };
            kbdHook.KeyUpEvent += (sender, vkCode, injected) =>
            {
                Console.WriteLine((Keys)vkCode + " Up" + (injected ? " (Injected)" : ""));
                return false;
            };

            msHook.MouseDownEvent += (sender, vkCode, x, y, delta, injected) =>
            {
                Console.WriteLine((Keys)vkCode + $" Down on ({x}, {y})" + (injected ? " (Injected)" : ""));
                return false;
            };
            msHook.MouseUpEvent += (sender, vkCode, x, y, delta, injected) =>
            {
                Console.WriteLine((Keys)vkCode + $" Up on ({x}, {y})" + (injected ? " (Injected)" : ""));
                return false;
            };
            msHook.MouseMoveEvent += (sender, vkCode, x, y, delta, injected) =>
            {
                Console.WriteLine($"Mouse Move to ({x}, {y})" + (injected ? " (Injected)" : ""));
                return false;
            };
            msHook.MouseWheelEvent += (sender, vkCode, x, y, delta, injected) =>
            {
                Console.WriteLine($"Mouse Wheel with data {delta} on ({x}, {y})" + (injected ? " (Injected)" : ""));
                return false;
            };
            msHook.MouseHWheelEvent += (sender, vkCode, x, y, delta, injected) =>
            {
                Console.WriteLine($"Mouse HWheel with data {delta} on ({x}, {y})" + (injected ? " (Injected)" : ""));
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
