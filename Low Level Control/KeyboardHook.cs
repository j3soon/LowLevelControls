using System;
using System.Runtime.InteropServices;
using Low_Level_Control.Natives;

namespace Low_Level_Control
{
    public class KeyboardHook : HookBase
    {
        //Return whether the key is handled.
        public delegate bool KeyboardEventHandler(KeyboardHook sender, uint vkCode);
        public event KeyboardEventHandler KeyDownEvent;
        public event KeyboardEventHandler KeyUpEvent;

        public KeyboardHook() : base((int)WH.KEYBOARD_LL) { }

        protected override IntPtr CustomHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            KBDLLHOOKSTRUCT kbd =
                (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            switch ((WM)wParam)
            {
                case WM.KEYDOWN:
                case WM.SYSKEYDOWN:
                    if (KeyDownEvent?.Invoke(this, kbd.vkCode) == true)
                        return (IntPtr)(-1);
                    break;
                case WM.KEYUP:
                case WM.SYSKEYUP:
                    if (KeyUpEvent?.Invoke(this, kbd.vkCode) == true)
                        return (IntPtr)(-1);
                    break;
            }
            return IntPtr.Zero;
        }
    }
}
