using System;
using System.Windows.Input;
using System.Collections.Generic;

namespace CMDR
{
    internal class KeyBind
    {
        public Key Key;
        public Action OnKeyUp;
        public Action OnKeyDown;
        public bool IsKeyDownTriggered { get; set; }

        public KeyBind(Key key, Action onKeyDown, Action onKeyUp)
        {
            Key = key;
            OnKeyDown = onKeyDown;
            OnKeyUp = onKeyUp;
        }
        public void Detect()
        {
            if (KeyBoard.IsKeyDown(Key))
            {
                if (!IsKeyDownTriggered)
                {
                    OnKeyDown();
                    IsKeyDownTriggered = true;
                }
            }
            else if (KeyBoard.IsKeyUp(Key))
            {
                if (IsKeyDownTriggered)
                {
                    if (OnKeyUp != null)
                    {
                        OnKeyUp();
                    }
                    IsKeyDownTriggered = false;
                }
            }
        }
    }

    public static partial class KeyListener
    {
        internal static List<KeyBind> KeyBinds = new List<KeyBind>();
        private static List<KeyBind> _triggeredKeys = new List<KeyBind>();

        public static void AddKeyBind(Key key, Action onKeyDown, Action onKeyUp = null)
        {
            KeyBinds.Add(new KeyBind(key, onKeyDown, onKeyUp));
        }
    }
}
