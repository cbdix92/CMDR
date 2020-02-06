using System;
using System.Collections.Generic;


namespace CMDR.Components
{
    public abstract class <T>Component
    {
        public ComponentType ID { get; private set; }
		public List<GameObject> Parents = new List<GameObject>();
		public Dictionary<GameObject, T> Parents;
        
        public Component<T>(ComponentType id)
        {
            ID = id;
			Parents = new Dictionary<GameObject, T>();
        }
    }
}
