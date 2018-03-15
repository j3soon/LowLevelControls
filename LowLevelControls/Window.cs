using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LowLevelControls.Natives;

namespace LowLevelControls
{
    public static class Window
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("dwmapi.dll")]
        static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, IntPtr pvAttribute, int cbAttribute);

        public static RECT GetWindowBounds(IntPtr handle)
        {
            RECT rect;
            //Below Vista (exclusive)
            if (Environment.OSVersion.Version.Major < 6)
            {
                if (!GetWindowRect(handle, out rect))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                return rect;
            }
            IntPtr ptrFrame = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RECT)));
            int ret = DwmGetWindowAttribute(handle, 9, ptrFrame, Marshal.SizeOf(typeof(RECT)));
            if (ret != 0)
                throw new Win32Exception(ret);
            rect = (RECT)Marshal.PtrToStructure(ptrFrame, typeof(RECT));
            Marshal.FreeHGlobal(ptrFrame);
            return rect;
        }

        public static Process GetProcessFromHandle(IntPtr handle)
        {
            uint pid;
            GetWindowThreadProcessId(handle, out pid);
            Process proc = Process.GetProcessById((int)pid);
            return proc;
        }
    }
}
