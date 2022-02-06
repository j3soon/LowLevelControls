using System.Runtime.InteropServices;

namespace LowLevelControls.Natives
{
    // To deal with difference in x86/x64 packing
    [StructLayout(LayoutKind.Explicit)]
    public struct MouseKeybdHardwareInputUnion
    {
        [FieldOffset(0)]
        public MOUSEINPUT mi;
        [FieldOffset(0)]
        public KEYBDINPUT ki;
        [FieldOffset(0)]
        public HARDWAREINPUT hi;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        public INPUTTYPE type;
        public MouseKeybdHardwareInputUnion mkhi;
    }
}
