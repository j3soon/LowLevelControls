using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeyLoggerSample
{
    public static class ExtensionMethods
    {
        public static int LowWord(this int num)
        { return (int)((uint)num & 0x0000FFFF); }
        public static int HighWord(this int num)
        { return num >> 16; }
    }
}
