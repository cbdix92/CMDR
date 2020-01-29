using System;
using System.Collections.Generic;


namespace CMDR
{
    public abstract class Component : IDisposable
    {
        public ComponentType ID { get; private set; }
        public virtual GameObject Parent { get; set; }
		public virtual List<GameObject> Parents = new List<GameObject>();
        
        public Component(ComponentType id)
        {
            ID = id;
        }
        // RenderData Methoods
        public virtual RenderState LoadFile(string src) { return null; }
        public virtual RenderState LoadImage(System.Drawing.Image image) { return null; }
        public virtual System.Drawing.Image GetRenderData(GameObject parent) { return null; }
        internal virtual void Init() { }

        // PhysicsConstraints Methods
        public virtual void CollisionOccured(GameObject collider) { }
        public virtual bool GetStatic() { return false; }

        // IDisposable Methods
        public virtual void Dispose() { }
    }
    internal class None : Component
    {
        internal None() : base (ComponentType.None)
        {
            throw new Exception("None ComponentType Exception: A component was added but does not exist or was never implemented!");
        }
    }
    public enum ComponentType
    {
        None,
        RenderData,
        PhysicsConstraints,
        StateMachine
    }
}
