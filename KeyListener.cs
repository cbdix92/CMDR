using System;
using System.Windows.Input;
using System.Collections.Generic;

namespace CMDR
{
    internal class KeyBind
    {
        public Key Key;
        public Action OnCall;
        public Action OnKeyUp;
        public Action OnKeyDown;
        public bool IsKeyDownTriggered { get; set; }

        public KeyBind(Key key, Action onKeyDown, Action onKeyUp)
        {
            Key = key;
            OnKeyDown = onKeyDown;
            OnKeyUp = onKeyUp;
        }
        public bool Detect()
        {
            if (Keyboard.IsKeyDown(Key))// && !IsKeyDownTriggered)
            {
                IsKeyDownTriggered = true;
                OnCall = OnKeyDown;
                return true;
            }
            else if (OnKeyUp != null && IsKeyDownTriggered && Keyboard.IsKeyUp(Key))
            {
                IsKeyDownTriggered = false;
                OnCall = OnKeyUp;
                return true;
            }
            return false;
        }
        public void Reset()
        {
            if (OnKeyUp == null) IsKeyDownTriggered = false;
            OnCall = null;
        }
    }

    public static class KeyListener
    {
        internal static List<KeyBind> KeyBinds = new List<KeyBind>();
        private static List<KeyBind> _triggeredKeys = new List<KeyBind>();

        public static void Init()
        {

        }
        public static void AddKeyBind(Key key, Action onKeyDown, Action onKeyUp = null)
        {
            KeyBinds.Add(new KeyBind(key, onKeyDown, onKeyUp));
        }
        public static void Listen(object caller, EventArgs e)
        {
            foreach (KeyBind KeyBind in KeyBinds)
            {
                if (KeyBind.Detect())
                {
                    _triggeredKeys.Add(KeyBind);
                }
            }
        }
        public static void HandlePressedKeys(object caller, EventArgs e)
        {
            foreach (KeyBind KeyBind in _triggeredKeys)
            {
                if (KeyBind.OnCall != null)
                {
                    KeyBind.OnCall();
                }
                KeyBind.Reset();
            }
            _triggeredKeys.Clear();
        }
    }
}
