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
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey);

        private static INPUT getInput(int key, bool down, bool unicode=false)
        {
            INPUT input = new INPUT
            {
                type = INPUTTYPE.INPUT_KEYBOARD,
            };
            if (unicode)
            {
                input.mkhi.ki = new KEYBDINPUT
                {
                    wVk = 0,
                    wScan = (ushort)key,
                    dwFlags = (uint)KEYEVENTF.UNICODE | (uint)(down ? KEYEVENTF.KEYDOWN : KEYEVENTF.KEYUP),
                    time = 0,
                    dwExtraInfo = IntPtr.Zero
                };
                return input;
            }
            input.mkhi.ki = new KEYBDINPUT
            {
                wVk = (ushort)key,
                wScan = 0,
                dwFlags = (uint)(down ? KEYEVENTF.KEYDOWN : KEYEVENTF.KEYUP),
                time = 0,
                dwExtraInfo = IntPtr.Zero
            };
            return input;
        }

        public static void SendKey(int vKey)
        {
            INPUT[] inputs = { getInput(vKey, true), getInput(vKey, false) };
            uint sent = SendInput(2, inputs, Marshal.SizeOf(typeof(INPUT)));
            if (sent != 2)
                throw new Win32Exception(Marshal.GetLastWin32Error(), sent.ToString());
        }

        public static void SendKeyDown(int vKey)
        {
            INPUT[] inputs = { getInput(vKey, true) };
            uint sent = SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
            if (sent != 1)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static void SendKeyUp(int vKey)
        {
            INPUT[] inputs = { getInput(vKey, false) };
            uint sent = SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
            if (sent != 1)
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
            INPUT[] inputs = { getInput(c, true, true), getInput(c, false, true) };
            uint sent = SendInput(2, inputs, Marshal.SizeOf(typeof(INPUT)));
            if (sent != 2)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static void SendText(String text)
        {
            int len = text.Length;
            INPUT[] inputs = new INPUT[2 * len];
            for (int i = 0; i < len; i++)
            {
                inputs[2*i] = getInput(text[i], true, true);
                inputs[2*i+1] = getInput(text[i], false, true);
            }
            uint sent = SendInput(2 * (uint)len, inputs, Marshal.SizeOf(typeof(INPUT)));
            if (sent != 2 * (uint)len)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }
    }
}
