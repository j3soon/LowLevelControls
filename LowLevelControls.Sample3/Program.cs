using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LowLevelControls.Sample3
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(3000);
            Keyboard.SendText("ABCDEFG");
        }
    }
}
