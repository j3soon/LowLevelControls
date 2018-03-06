using System.Runtime.InteropServices;

namespace LowLevelControls.Natives
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x, y;
    }
}
