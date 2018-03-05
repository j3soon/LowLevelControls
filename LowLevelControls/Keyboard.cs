using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LowLevelControls.Natives;

namespace LowLevelControls
{
    public static class Keyboard
    {
        [DllImport("user32.dll")]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey);

        public static void SendKey(int vKey)
        {
            INPUT[] inputs = new INPUT[2];
            inputs[0].type = INPUTTYPE.INPUT_KEYBOARD;
            inputs[0].ki = new KEYBDINPUT
            {
                wVk = (ushort)vKey,
                wScan = 0,
                dwFlags = (uint)KEYEVENTF.KEYDOWN,
                time = 0,
                dwExtraInfo = IntPtr.Zero
            };
            inputs[1].type = INPUTTYPE.INPUT_KEYBOARD;
            inputs[1].ki = new KEYBDINPUT
            {
                wVk = (ushort)vKey,
                wScan = 0,
                dwFlags = (uint)KEYEVENTF.KEYUP,
                time = 0,
                dwExtraInfo = IntPtr.Zero
            };
            if (SendInput(2, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static void SendKeyDown(int vKey)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = INPUTTYPE.INPUT_KEYBOARD;
            inputs[0].ki = new KEYBDINPUT
            {
                wVk = (ushort)vKey,
                wScan = 0,
                dwFlags = (uint)KEYEVENTF.KEYDOWN,
                time = 0,
                dwExtraInfo = IntPtr.Zero
            };
            if (SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static void SendKeyUp(int vKey)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = INPUTTYPE.INPUT_KEYBOARD;
            inputs[0].ki = new KEYBDINPUT
            {
                wVk = (ushort)vKey,
                wScan = 0,
                dwFlags = (uint)KEYEVENTF.KEYUP,
                time = 0,
                dwExtraInfo = IntPtr.Zero
            };
            if (SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static bool IsKeyDown(int vKey)
        {
            return (GetKeyState(vKey) & 0x80) != 0;
        }

        public static bool IsKeyToggled(int vKey)
        {
            return (GetKeyState(vKey) & 0x01) != 0;
        }

        public static void SendChar(char c)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = INPUTTYPE.INPUT_KEYBOARD;
            inputs[0].ki = new KEYBDINPUT
            {
                wVk = 0,
                wScan = c,
                dwFlags = (uint)KEYEVENTF.UNICODE,
                time = 0,
                dwExtraInfo = IntPtr.Zero
            };
            if (SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static void SendText(String text)
        {
            int len = text.Length;
            INPUT[] inputs = new INPUT[len];
            for (int i = 0; i < len; i++)
            {
                inputs[i].type = INPUTTYPE.INPUT_KEYBOARD;
                inputs[i].ki = new KEYBDINPUT
                {
                    wVk = 0,
                    wScan = text[i],
                    dwFlags = (uint)KEYEVENTF.UNICODE,
                    time = 0,
                    dwExtraInfo = IntPtr.Zero
                };
            }
            if (SendInput((uint)len, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }
    }
}
