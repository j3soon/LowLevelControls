using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Low_Level_Control;
using Low_Level_Control.Natives;

namespace KeyLoggerSample
{
    static class Program
    {
        static void Main(string[] args)
        {
            KeyboardHook kbdHook = new KeyboardHook();
            kbdHook.NativeHookProcEvent += delegate (int nCode, IntPtr wParam, IntPtr lParam)
            {
                KBDLLHOOKSTRUCT kbd =
                    (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                switch ((WM)wParam)
                {
                    case WM.KEYDOWN:
                    case WM.SYSKEYDOWN:
                        Console.WriteLine((Keys)kbd.vkCode + " Down");
                        break;
                    case WM.KEYUP:
                    case WM.SYSKEYUP:
                        Console.WriteLine((Keys)kbd.vkCode + " Up");
                        break;
                }
                return IntPtr.Zero;
            };
            MouseHook msHook = new MouseHook();
            msHook.NativeHookProcEvent += delegate (int nCode, IntPtr wParam, IntPtr lParam)
            {
                MSLLHOOKSTRUCT ms =
                    (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                switch ((WM)wParam)
                {
                    case WM.LBUTTONDOWN:
                        Console.WriteLine(Keys.LButton + $" Down on ({ms.pt.x}, {ms.pt.y})");
                        break;
                    case WM.LBUTTONUP:
                        Console.WriteLine(Keys.LButton + $" Up on ({ms.pt.x}, {ms.pt.y})");
                        break;
                    case WM.MOUSEMOVE:
                        Console.WriteLine($"Mouse Move to ({ms.pt.x}, {ms.pt.y})");
                        break;
                    case WM.MOUSEWHEEL:
                        Console.WriteLine($"Mouse Wheel with data {ms.mouseData.HighWord()}");
                        break;
                    case WM.MOUSEHWHEEL:
                        //Not implemented yet.
                        break;
                    case WM.RBUTTONDOWN:
                        Console.WriteLine(Keys.RButton + $" Down on ({ms.pt.x}, {ms.pt.y})");
                        break;
                    case WM.RBUTTONUP:
                        Console.WriteLine(Keys.RButton + $" Up on ({ms.pt.x}, {ms.pt.y})");
                        break;
                    case WM.MBUTTONDOWN:
                        Console.WriteLine(Keys.MButton + $" Down on ({ms.pt.x}, {ms.pt.y})");
                        break;
                    case WM.MBUTTONUP:
                        Console.WriteLine(Keys.MButton + $" Up on ({ms.pt.x}, {ms.pt.y})");
                        break;
                    case WM.XBUTTONDOWN:
                        if (ms.mouseData.HighWord() == (int)MOUSEDATA.XBUTTON1)
                            Console.Write(Keys.XButton1);
                        else if (ms.mouseData.HighWord() == (int)MOUSEDATA.XBUTTON2)
                            Console.Write(Keys.XButton2);
                        Console.WriteLine($" Down on ({ms.pt.x}, {ms.pt.y})");
                        break;
                    case WM.XBUTTONUP:
                        if (ms.mouseData.HighWord() == (int)MOUSEDATA.XBUTTON1)
                            Console.Write(Keys.XButton1);
                        else if (ms.mouseData.HighWord() == (int)MOUSEDATA.XBUTTON2)
                            Console.Write(Keys.XButton2);
                        Console.WriteLine($" Up on ({ms.pt.x}, {ms.pt.y})");
                        break;
                }
                return IntPtr.Zero;
            };
            kbdHook.InstallGlobalHook();
            msHook.InstallGlobalHook();
            for (;;)
            {
                Thread.Sleep(1);
                System.Windows.Forms.Application.DoEvents();
            }
        }
    }
}
