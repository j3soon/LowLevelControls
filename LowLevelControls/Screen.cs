using System;
using System.Collections.Generic;
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

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

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
                left = GetSystemMetrics((int) SM.XVIRTUALSCREEN),
                top = GetSystemMetrics((int) SM.YVIRTUALSCREEN)
            };
            rect.right = GetSystemMetrics((int)SM.CXSCREEN) - rect.left;
            rect.bottom = GetSystemMetrics((int)SM.CYSCREEN) - rect.top;
            return rect;
        }

        public static void SetMonitorOn()
        {
            SendMessage(HWND_BROADCAST, (uint)WM.SYSCOMMAND, SC_MONITORPOWER, MONITOR_ON);
        }

        public static void SetMonitorOff()
        {
            SendMessage(HWND_BROADCAST, (uint)WM.SYSCOMMAND, SC_MONITORPOWER, MONITOR_OFF);
        }

        public static void SetMonitorStandby()
        {
            SendMessage(HWND_BROADCAST, (uint)WM.SYSCOMMAND, SC_MONITORPOWER, MONITOR_STANDBY);
        }
    }
}
