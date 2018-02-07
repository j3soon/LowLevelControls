using Low_Level_Control.Natives;

namespace Low_Level_Control
{
    public class KeyboardHook : HookBase
    {
        public KeyboardHook() : base((int)WH.KEYBOARD_LL, "Keyboard") { }
    }
}
