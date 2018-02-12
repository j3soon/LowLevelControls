using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Low_Level_Control.Natives;

namespace Low_Level_Control
{
    public abstract class HookBase
    {
        [DllImport("user32.dll", SetLastError = true)]
        protected static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        protected static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        protected static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll", SetLastError = true)]
        protected static extern IntPtr GetModuleHandle(string lpModuleName);

        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        public event HookProc NativeHookProcEvent;
        public bool hookInstalled { get; private set; } = false;

        private HookProc hookProcDelegate = null;
        private IntPtr hookHandle = IntPtr.Zero;
        private int hookID = 0;
        private String hookType => ((WH)hookID).ToString();

        /*protected String getExceptionMessage(String msg)
        {
            return HookType + " hook: " + msg;
        }*/

        public HookBase(int hookID)
        {
            this.hookID = hookID;
        }

        public void InstallGlobalHook()
        {
            if (hookInstalled)
                throw new HookException(hookType + " hook has already been installed.");
            hookInstalled = true;
            hookProcDelegate = LowLevelHookProc;
            IntPtr moduleHandle = GetModuleHandle(null);
            if (moduleHandle == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error(), "GetModuleHandle failed.");
            hookHandle = SetWindowsHookEx(hookID, hookProcDelegate, moduleHandle, 0);
            if (hookHandle == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error(), "SetWindowsHookEx failed.");
        }

        protected IntPtr LowLevelHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if (NativeHookProcEvent != null)
                {
                    if (NativeHookProcEvent(nCode, wParam, lParam) == (IntPtr)(-1))
                        return (IntPtr)(-1);
                }
                if (CustomHookProc(nCode, wParam, lParam) == (IntPtr)(-1))
                    return (IntPtr)(-1);
            }
            return CallNextHookEx(hookHandle, nCode, wParam, lParam);
        }

        protected virtual IntPtr CustomHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            return IntPtr.Zero;
        }

        public void UninstallGlobalKeyboardHook()
        {
            if (!hookInstalled)
                throw new HookException(hookType + " hook has not been installed.");
            hookInstalled = false;
            if (!UnhookWindowsHookEx(hookHandle))
                throw new Win32Exception(Marshal.GetLastWin32Error(), "UnhookWindowsHookEx failed.");
            hookHandle = IntPtr.Zero;
            hookProcDelegate = null;
        }

        public static int LowWord(int num)
        { return num & 0x0000FFFF; }

        //Assume it won't be negative.
        public static int HighWord(int num)
        { return num >> 16; }
    }
}
