using System;
using System.Runtime.InteropServices;
using LowLevelControls.Natives;

namespace LowLevelControls
{
    public class MouseHook : HookBase
    {
        //Return whether the key is handled.
        public delegate bool MouseEventHandler(MouseHook sender, uint vkCode, int x, int y, int delta);
        public event MouseEventHandler MouseDownEvent;
        public event MouseEventHandler MouseUpEvent;
        public event MouseEventHandler MouseMoveEvent;
        public event MouseEventHandler MouseWheelEvent;

        public MouseHook() : base((int)WH.MOUSE_LL) { }

        protected override IntPtr CustomHookProc(IntPtr wParam, IntPtr lParam)
        {
            MSLLHOOKSTRUCT ms =
                (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            switch ((WM)wParam)
            {
                case WM.LBUTTONDOWN:
                    if (MouseDownEvent?.Invoke(this, (uint)VK.LBUTTON, ms.pt.x, ms.pt.y, 0) == true)
                        return (IntPtr)(-1);
                    break;
                case WM.LBUTTONUP:
                    if (MouseUpEvent?.Invoke(this, (uint)VK.LBUTTON, ms.pt.x, ms.pt.y, 0) == true)
                        return (IntPtr)(-1);
                    break;
                case WM.MOUSEMOVE:
                    if (MouseMoveEvent?.Invoke(this, 0, ms.pt.x, ms.pt.y, 0) == true)
                        return (IntPtr) (-1);
                    break;
                case WM.MOUSEWHEEL:
                    if (MouseWheelEvent?.Invoke(this, 0, ms.pt.x, ms.pt.y, HighWord(ms.mouseData)) == true)
                        return (IntPtr)(-1);
                    break;
                case WM.MOUSEHWHEEL:
                    //Not implemented yet.
                    break;
                case WM.RBUTTONDOWN:
                    if (MouseDownEvent?.Invoke(this, (uint)VK.RBUTTON, ms.pt.x, ms.pt.y, 0) == true)
                        return (IntPtr)(-1);
                    break;
                case WM.RBUTTONUP:
                    if (MouseUpEvent?.Invoke(this, (uint)VK.RBUTTON, ms.pt.x, ms.pt.y, 0) == true)
                        return (IntPtr)(-1);
                    break;
                case WM.MBUTTONDOWN:
                    if (MouseDownEvent?.Invoke(this, (uint)VK.MBUTTON, ms.pt.x, ms.pt.y, 0) == true)
                        return (IntPtr)(-1);
                    break;
                case WM.MBUTTONUP:
                    if (MouseUpEvent?.Invoke(this, (uint)VK.MBUTTON, ms.pt.x, ms.pt.y, 0) == true)
                        return (IntPtr)(-1);
                    break;
                case WM.XBUTTONDOWN:
                    if (HighWord(ms.mouseData) == (int) MOUSEDATA.XBUTTON1)
                    {
                        if (MouseDownEvent?.Invoke(this, (uint)VK.XBUTTON1, ms.pt.x, ms.pt.y, 0) == true)
                            return (IntPtr)(-1);
                    }
                    else if (HighWord(ms.mouseData) == (int) MOUSEDATA.XBUTTON2)
                    {
                        if (MouseDownEvent?.Invoke(this, (uint)VK.XBUTTON2, ms.pt.x, ms.pt.y, 0) == true)
                            return (IntPtr)(-1);
                    }
                    break;
                case WM.XBUTTONUP:
                    if (HighWord(ms.mouseData) == (int)MOUSEDATA.XBUTTON1)
                    {
                        if (MouseUpEvent?.Invoke(this, (uint)VK.XBUTTON1, ms.pt.x, ms.pt.y, 0) == true)
                            return (IntPtr)(-1);
                    }
                    else if (HighWord(ms.mouseData) == (int)MOUSEDATA.XBUTTON2)
                    {
                        if (MouseUpEvent?.Invoke(this, (uint)VK.XBUTTON2, ms.pt.x, ms.pt.y, 0) == true)
                            return (IntPtr)(-1);
                    }
                    break;
            }
            return IntPtr.Zero;
        }
    }
}
