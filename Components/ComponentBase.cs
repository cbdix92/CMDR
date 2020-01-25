using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace CMDR.Components
{
    public abstract class Component : IDisposable
    {
        public ComponentType ID { get; private set; }
        public GameObject Parent;
        
        public Component(ComponentType id)
        {
            ID = id;
        }
        public virtual System.Drawing.Image GetRenderData() { return null; }
        public virtual void Dispose() { return; }
    }
    public enum ComponentType
    {
        Image,
        Animation,
        PhysicsConstraints,
        StateMachine
    }
}
