using System;
using System.Runtime.InteropServices;
using LowLevelControls.Natives;

namespace LowLevelControls
{
    public class KeyboardHook : HookBase
    {
        [DllImport("user32.dll")]
        protected static extern short GetAsyncKeyState(int vKey);

        //Return whether the key is handled.
        public delegate bool KeyboardEventHandler(KeyboardHook sender, uint vkCode, bool injected);
        public event KeyboardEventHandler KeyDownEvent;
        public event KeyboardEventHandler KeyPressEvent;
        public event KeyboardEventHandler KeyUpEvent;

        public KeyboardHook() : base((int)WH.KEYBOARD_LL) { }

        protected override IntPtr CustomHookProc(IntPtr wParam, IntPtr lParam)
        {
            KBDLLHOOKSTRUCT kbd =
                (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            bool injected = (kbd.flags & (uint)LLKHF.INJECTED) != 0;
            switch ((WM)wParam)
            {
                case WM.KEYDOWN:
                case WM.SYSKEYDOWN:
                    if (GetAsyncKeyState((int)kbd.vkCode) >= 0 && KeyDownEvent?.Invoke(this, kbd.vkCode, injected) == true)
                        return (IntPtr)(-1);
                    if (KeyPressEvent?.Invoke(this, kbd.vkCode, injected) == true)
                        return (IntPtr)(-1);
                    break;
                case WM.KEYUP:
                case WM.SYSKEYUP:
                    if (KeyUpEvent?.Invoke(this, kbd.vkCode, injected) == true)
                        return (IntPtr)(-1);
                    break;
            }
            return IntPtr.Zero;
        }
    }
}
