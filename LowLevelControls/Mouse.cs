using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LowLevelControls.Natives;

namespace LowLevelControls
{
    public static class Mouse
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int nIndex);

        private static INPUT getInput(int vKey, bool? down)
        {
            MOUSEDATA data = 0;
            MOUSEEVENTF flags = 0;
            switch ((VK)vKey)
            {
                case VK.LBUTTON:
                    if (down == null)
                    {
                        flags = MOUSEEVENTF.LEFTDOWN | MOUSEEVENTF.LEFTUP;
                        break;
                    }
                    flags = (bool)down ? MOUSEEVENTF.LEFTDOWN : MOUSEEVENTF.LEFTUP;
                    break;
                case VK.RBUTTON:
                    if (down == null)
                    {
                        flags = MOUSEEVENTF.RIGHTDOWN | MOUSEEVENTF.RIGHTUP;
                        break;
                    }
                    flags = (bool)down ? MOUSEEVENTF.RIGHTDOWN : MOUSEEVENTF.RIGHTUP;
                    break;
                case VK.MBUTTON:
                    if (down == null)
                    {
                        flags = MOUSEEVENTF.MIDDLEDOWN | MOUSEEVENTF.MIDDLEUP;
                        break;
                    }
                    flags = (bool)down ? MOUSEEVENTF.MIDDLEDOWN : MOUSEEVENTF.MIDDLEUP;
                    break;
                case VK.XBUTTON1:
                    data = MOUSEDATA.XBUTTON1;
                    if (down == null)
                    {
                        flags = MOUSEEVENTF.XDOWN | MOUSEEVENTF.XUP;
                        break;
                    }
                    flags = (bool)down ? MOUSEEVENTF.XDOWN : MOUSEEVENTF.XUP;
                    break;
                case VK.XBUTTON2:
                    data = MOUSEDATA.XBUTTON2;
                    if (down == null)
                    {
                        flags = MOUSEEVENTF.XDOWN | MOUSEEVENTF.XUP;
                        break;
                    }
                    flags = (bool)down ? MOUSEEVENTF.XDOWN : MOUSEEVENTF.XUP;
                    break;
            }
            INPUT input = new INPUT
            {
                type = INPUTTYPE.INPUT_MOUSE,
                mkhi = new MouseKeybdHardwareInputUnion()
                {
                    mi = new MOUSEINPUT()
                    {
                        dx = 0,
                        dy = 0,
                        mouseData = (int)data,
                        dwFlags = (uint)flags,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };
            return input;
        }

        private static void setMouseInput(ref MOUSEINPUT mi, int? x, int? y)
        {
            if (x == null || y == null)
                return;
            RECT screen = Screen.GetVirtualScreenRect();
            mi.dx = (int)((x - screen.left) * 65536 / (screen.right - screen.left)) + 1;
            mi.dy = (int)((y - screen.top) * 65536 / (screen.bottom - screen.top)) + 1;
            mi.dwFlags = (uint)(MOUSEEVENTF.MOVE | MOUSEEVENTF.ABSOLUTE | MOUSEEVENTF.VIRTUALDESK);
        }

        public static void SendMouseClick(int vKey, int? x = null, int? y = null)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0] = getInput(vKey, null);
            setMouseInput(ref inputs[0].mkhi.mi, x, y);
            if (SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static void SendMouseDown(int vKey, int? x = null, int? y = null)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0] = getInput(vKey, true);
            setMouseInput(ref inputs[0].mkhi.mi, x, y);
            if (SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static void SendMouseUp(int vKey, int? x = null, int? y = null)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0] = getInput(vKey, false);
            setMouseInput(ref inputs[0].mkhi.mi, x, y);
            if (SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static void SendMouseMove(int x, int y)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0] = getInput(0, false);
            setMouseInput(ref inputs[0].mkhi.mi, x, y);
            if (SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static void SendMouseWheel(int delta, int? x = null, int? y = null)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0] = getInput(0, false);
            inputs[0].mkhi.mi.mouseData = delta;
            inputs[0].mkhi.mi.dwFlags = (uint)MOUSEEVENTF.WHEEL;
            setMouseInput(ref inputs[0].mkhi.mi, x, y);
            if (SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static void SendMouseHWheel(int delta, int? x = null, int? y = null)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0] = getInput(0, false);
            inputs[0].mkhi.mi.mouseData = delta;
            inputs[0].mkhi.mi.dwFlags = (uint)MOUSEEVENTF.HWHEEL;
            setMouseInput(ref inputs[0].mkhi.mi, x, y);
            if (SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static bool IsMouseDown(int vKey)
        {
            return (GetKeyState(vKey) & 0x80) != 0;
        }

        public static bool IsMouseButtonSwapped()
        {
            return GetSystemMetrics((int)SM.SWAPBUTTON) != 0;
        }

        public static void SetMousePosition(int x, int y)
        {
            if (!SetCursorPos(x, y))
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static POINT GetMousePosition()
        {
            POINT point;
            if (!GetCursorPos(out point))
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return point;
        }
    }
}
