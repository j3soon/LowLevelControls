using System;
using System.Runtime.InteropServices;

namespace LowLevelControls.Natives
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MSLLHOOKSTRUCT
    {
        public POINT pt;
        public int mouseData;
        public uint flags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }
}
