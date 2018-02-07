using System.Runtime.InteropServices;

namespace Low_Level_Control.Natives
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }
}
