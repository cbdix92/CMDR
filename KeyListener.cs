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
        private DateTime DateTime;

        public KeyBind(Key key, Action onKeyDown, Action onKeyUp)
        {
            Key = key;
            OnKeyDown = onKeyDown;
            if (onKeyUp != null)
            {
                OnKeyUp = onKeyUp;
            }
            else
            {
                onKeyUp = KeyUpReset;
            }
            DateTime = new DateTime();
        }
        public bool Detect()
        {
            if (Keyboard.IsKeyDown(Key))// && !IsKeyDownTriggered)
            {
                if (!IsKeyDownTriggered)
                {
                    WhenKeyDownTriggered = DateTime.Ticks;
                    IsKeyDownTriggered = true;
                }
                OnCall = OnKeyDown;
                return true;
            }
            else if (Keyboard.IsKeyUp(Key) && IsKeyDownTriggered)
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
        private void KeyUpReset()
        {
            // Only used if the "onKeyUp" is never passed to the constructor
            IsKeyDownTriggered = false;
        }
    }

    public class KeyListener
    {
        internal static List<KeyBind> KeyBinds = new List<KeyBind>();
        private static List<KeyBind> _triggeredKeys = new List<KeyBind>();

        public KeyListener CKeyListener = new KeyListener();
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
            // Sort _triggeredKeys KeyBind.WhenKeyDownTriggered values in ascending order
            long[] SortedArray = new long[_triggeredKeys.Count];
            for (int i = 0; i < _triggeredKeys.Count-1; i++)
            {
                SortedArray[i] = _triggeredKeys[i].WhenKeyDownTriggered;
            }
            Array.Sort(SortedArray);

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
