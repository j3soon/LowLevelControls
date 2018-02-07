
namespace Low_Level_Control.Natives
{
    /// <summary>
    /// <para>Only lists some used constants.</para>
    /// <para>See https://msdn.microsoft.com/en-us/library/ms644927(v=VS.85).aspx#system_defined for more information.</para>
    /// </summary>
    public enum WM : uint
    {
        //Keyboard Messages.
        KEYDOWN = 0x0100,
        KEYUP = 0x0101,
        SYSKEYDOWN = 0x0104,
        SYSKEYUP = 0x0105,
        //Mouse Messages.
        LBUTTONDBLCLK = 0x0203,
        LBUTTONDOWN = 0x0201,
        LBUTTONUP = 0x0202,
        MOUSEMOVE = 0x0200,
        MOUSEWHEEL = 0x020A,
        MOUSEHWHEEL = 0x020E,
        RBUTTONDBLCLK = 0x0206,
        RBUTTONDOWN = 0x0204,
        RBUTTONUP = 0x0205,
        MBUTTONDBLCLK = 0x0209,
        MBUTTONDOWN = 0x0207,
        MBUTTONUP = 0x0208,
        XBUTTONDBLCLK = 0x020D,
        XBUTTONDOWN = 0x020B,
        XBUTTONUP = 0x020C,
    }
}
