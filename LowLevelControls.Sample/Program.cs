using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using LowLevelControls.Natives;

namespace LowLevelControls.Sample
{
    static class Program
    {
        static KeyboardHook kbdHook = new KeyboardHook();
        static MouseHook msHook = new MouseHook();

        static void Main(string[] args)
        {
            kbdHook.NativeHookProcEvent += KbdHook_NativeHookProcEvent;
            msHook.NativeHookProcEvent += MsHook_NativeHookProcEvent;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            kbdHook.InstallGlobalHook();
            msHook.InstallGlobalHook();
            for (;;)
            {
                Thread.Sleep(1);
                Application.DoEvents();
            }
        }

        private static IntPtr KbdHook_NativeHookProcEvent(int nCode, IntPtr wParam, IntPtr lParam)
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
        }

        private static IntPtr MsHook_NativeHookProcEvent(int nCode, IntPtr wParam, IntPtr lParam)
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
                    Console.WriteLine($"Mouse Wheel with data {HookBase.HighWord(ms.mouseData)} on ({ms.pt.x}, {ms.pt.y})");
                    break;
                case WM.MOUSEHWHEEL:
                    Console.WriteLine($"Mouse HWheel with data {HookBase.HighWord(ms.mouseData)} on ({ms.pt.x}, {ms.pt.y})");
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
                    if (HookBase.HighWord(ms.mouseData) == (int)MOUSEDATA.XBUTTON1)
                        Console.Write(Keys.XButton1);
                    else if (HookBase.HighWord(ms.mouseData) == (int)MOUSEDATA.XBUTTON2)
                        Console.Write(Keys.XButton2);
                    Console.WriteLine($" Down on ({ms.pt.x}, {ms.pt.y})");
                    break;
                case WM.XBUTTONUP:
                    if (HookBase.HighWord(ms.mouseData) == (int)MOUSEDATA.XBUTTON1)
                        Console.Write(Keys.XButton1);
                    else if (HookBase.HighWord(ms.mouseData) == (int)MOUSEDATA.XBUTTON2)
                        Console.Write(Keys.XButton2);
                    Console.WriteLine($" Up on ({ms.pt.x}, {ms.pt.y})");
                    break;
            }
            return IntPtr.Zero;
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            kbdHook.UninstallGlobalHook();
            msHook.UninstallGlobalHook();
        }
    }
}
