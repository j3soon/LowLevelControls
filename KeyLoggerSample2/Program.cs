using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Low_Level_Control;
using Low_Level_Control.Natives;

namespace KeyLoggerSample2
{
    static class Program
    {
        static void Main(string[] args)
        {
            KeyboardHook kbdHook = new KeyboardHook();
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
            MouseHook msHook = new MouseHook();
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
            kbdHook.InstallGlobalHook();
            msHook.InstallGlobalHook();
            for (;;)
            {
                Thread.Sleep(1);
                Application.DoEvents();
            }
        }
    }
}
