using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace CMDR
{
    public abstract class Component
    {
        public ComponentType ID { get; private set; }
        public GameObject Parent;
        
        public Component(ComponentType id)
        {
            ID = id;
        }
        public virtual System.Drawing.Image GetRenderData() { return null; }
    }
    public enum ComponentType
    {
        Image,
        Animation,
        StateMachine
    }
}
