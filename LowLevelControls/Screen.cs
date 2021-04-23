using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LowLevelControls.Natives;

namespace LowLevelControls
{
    public static class Screen
    {
        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        static IntPtr HWND_BROADCAST => new IntPtr(0xffff);
        static IntPtr SC_MONITORPOWER => new IntPtr(0xF170);
        static IntPtr MONITOR_ON => new IntPtr(-1);
        static IntPtr MONITOR_OFF => new IntPtr(2);
        static IntPtr MONITOR_STANDBY => new IntPtr(1);


        public static int GetScreenCount()
        {
            return GetSystemMetrics((int)SM.CMONITORS);
        }

        public static RECT GetScreenRect()
        {
            RECT rect = new RECT()
            {
                left = 0,
                top = 0,
                right = GetSystemMetrics((int)SM.CXSCREEN),
                bottom = GetSystemMetrics((int)SM.CYSCREEN)
            };
            return rect;
        }

        public static RECT GetVirtualScreenRect()
        {
            RECT rect = new RECT
            {
                left = GetSystemMetrics((int)SM.XVIRTUALSCREEN),
                top = GetSystemMetrics((int)SM.YVIRTUALSCREEN)
            };
            rect.right = GetSystemMetrics((int)SM.CXSCREEN) + rect.left;
            rect.bottom = GetSystemMetrics((int)SM.CYSCREEN) + rect.top;
            return rect;
        }

        public static RECT GetScaledVirtualScreenRect()
        {
            RECT rect = new RECT
            {
                left = GetSystemMetrics((int)SM.XVIRTUALSCREEN),
                top = GetSystemMetrics((int)SM.YVIRTUALSCREEN)
            };
            rect.right = (int)(GetSystemMetrics((int)SM.CXSCREEN) * GetScreenScaling()) + rect.left;
            rect.bottom = (int)(GetSystemMetrics((int)SM.CYSCREEN) * GetScreenScaling()) + rect.top;
            return rect;
        }
        public static float GetScreenScaling()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DEVICECAP.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DEVICECAP.DESKTOPVERTRES);
            return (float)PhysicalScreenHeight / (float)LogicalScreenHeight;
        }

        public static void SetMonitorOn()
        {
            if (SendMessage(HWND_BROADCAST, (uint)WM.SYSCOMMAND, SC_MONITORPOWER, MONITOR_ON) != IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static void SetMonitorOff()
        {
            if (SendMessage(HWND_BROADCAST, (uint)WM.SYSCOMMAND, SC_MONITORPOWER, MONITOR_OFF) != IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static void SetMonitorStandby()
        {
            if (SendMessage(HWND_BROADCAST, (uint)WM.SYSCOMMAND, SC_MONITORPOWER, MONITOR_STANDBY) != IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }
    }
}
