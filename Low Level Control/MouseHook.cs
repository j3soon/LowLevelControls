using Low_Level_Control.Natives;

namespace Low_Level_Control
{
    public class MouseHook : HookBase
    {
        public MouseHook() : base((int)WH.MOUSE_LL, "Mouse") { }
    }
}
