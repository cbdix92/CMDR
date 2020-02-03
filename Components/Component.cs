using System;
using System.Collections.Generic;


namespace CMDR.Components
{
    public abstract class Component
    {
        public ComponentType ID { get; private set; }
		public List<GameObject> Parents = new List<GameObject>();
        
        public Component(ComponentType id)
        {
            ID = id;
        }
    }
    internal class None : Component
    {
        internal None() : base (ComponentType.None)
        {
            throw new Exception("None ComponentType Exception: A component was added but does not exist or was never implemented!");
        }
    }
}
