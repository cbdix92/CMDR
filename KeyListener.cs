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

        public long WhenKeyDownTriggered;

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
                if (!IsKeyDownTriggered)
                {
                    WhenKeyDownTriggered = DateTime.Ticks;
                }
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

    public class KeyListener
    {
        internal static List<KeyBind> KeyBinds = new List<KeyBind>();
        private static List<KeyBind> _triggeredKeys = new List<KeyBind>();

        public KeyListener KeyListener = new KeyListener();
        private KeyListener()
        {

        }
        public static void AddKeyBind(Key key, Action onKeyDown, Action onKeyUp = null)
        {
            KeyBinds.Add(new KeyBind(key, onKeyDown, onKeyUp));
        }
        public static void Listen(object caller, EventArgs e)
        {
            // In the future. Combine Listen and HandlePressedKeys. 
            // Since physics updates happen on seperate ticks there is no reason to not do these two methods at the same time on the same tick..
            // and place the input events on the main update loop.
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
