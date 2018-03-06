using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowLevelControls.Natives
{
    /// <summary>
    /// <para>Only lists some used constants.</para>
    /// <para>See https://msdn.microsoft.com/en-us/library/windows/desktop/ms724385(v=vs.85).aspx for more information.</para>
    /// </summary>
    public enum SM
    {
        CMONITORS = 80,
        CXSCREEN = 0,
        CXVIRTUALSCREEN = 78,
        CYSCREEN = 1,
        CYVIRTUALSCREEN = 79,
        SWAPBUTTON = 23,
        XVIRTUALSCREEN = 76,
        YVIRTUALSCREEN = 77,
    }
}
