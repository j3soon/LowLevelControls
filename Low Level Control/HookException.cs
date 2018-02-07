using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Low_Level_Control
{
    public class HookException : Exception
    {
        public HookException(string message) : base(message)
        {
        }
    }
}
