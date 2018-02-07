namespace Low_Level_Control.Natives
{
    /// <summary>
    /// <para>The origin constant don't have the 'MOUSEDATA' prefix.</para>
    /// <para>See https://msdn.microsoft.com/en-us/library/windows/desktop/ms644970(v=vs.85).aspx for more information.</para>
    /// </summary>
    public enum MOUSEDATA : int
    {
        XBUTTON1 = 0x01,
        XBUTTON2 = 0x02,
        WHEEL_DELTA = 0x78,
        //WHEEL_DELTA_REV = -0x78
    }
}
