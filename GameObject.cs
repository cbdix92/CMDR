using System;
using System.Collections.Generic;

namespace CMDR
{
    public class GameObject : IDisposable
    {
        public Scene Parent;
        public Transform Transform;
        internal List<Cell> OverlappedCells;
        internal List<GameObject> CenterCell;
        public Dictionary<ComponentType, Component> Components = new Dictionary<ComponentType, Component>();
        public int Hash { get => this.GetHashCode(); }

        private int _width;
        private int _height;
        public int Width
        {
            get
            {
                if (_width != 0)
                {
                    return _width;
                }
                try
                {
                    return Components[ComponentType.RenderData].GetRenderData(this).Width;
                }
                catch
                {
                    return 1;
                }
            }
            set
            {
                _width = value;
                if (SpatialIndexer.CellSize < value)
                {
                    SpatialIndexer.CellSize = value;
                }
            }
        }
        public int Height
        {
            get
            {
                if (_height != 0)
                {
                    return _height;
                }
                try
                {
                    return Components[ComponentType.RenderData].GetRenderData(this).Height;
                }
                catch
                {
                    return 1;
                }
            }
            set
            {
                _height = value;
                if (SpatialIndexer.CellSize < value)
                {
                    SpatialIndexer.CellSize = value;
                }
            }
        }
        private bool _active;
        public bool Active
        {
            get => _active;
            set
            {
                if (value && !_active && !Static) Parent.ActiveGameObjects.Add(this);
                else if (!value && _active) Parent.ActiveGameObjects.Remove(this);
                _active = value;
            }
        }
		
		public bool Static { get; internal set; }
        public bool Collider { get; internal set; }
		
        public bool Disposed { get; private set; }

        public GameObject(Scene parent, float posX, float posY, int posZ)
        {
            Parent = parent;
            Transform = new Transform(this, posX, posY, posZ);

            OverlappedCells = new List<Cell>();
            CenterCell = new List<GameObject>();
        }
        public void Use(Component component)
        {
            // Preset components are added here
            // ...
            if (Components.ContainsKey(component.ID))
            {
                Components.Remove(component.ID);
            }
            Components.Add(component.ID, component);
            switch (component.ID)
            {
                case ComponentType.RenderData:
                    break;

                case ComponentType.PhysicsConstraints:
                    Components[component.ID].NewParent(this);
                    break;

                case ComponentType.StateMachine:
                    AddComponent(new StateMachine());
                    break;
            }
        }
        public Component AddComponent(ComponentType componentType)
        {
            switch(componentType)
            {
                case ComponentType.RenderData:
                    AddComponent(new RenderData());
                    return Components[ComponentType.RenderData];
                
                case ComponentType.PhysicsConstraints:
                    AddComponent(new PhysicsConstraints(Parent));
                    return Components[ComponentType.PhysicsConstraints];
                
                case ComponentType.StateMachine:
                    AddComponent(new StateMachine());
                    return Components[ComponentType.StateMachine];
            }
            return new None();
        }
        public void AddComponent(Component component)
        {
            if (Components.ContainsKey(component.ID))
            {
                Components.Remove(component.ID);
            }
            Components.Add(component.ID, component);
        }
        public System.Drawing.Image GetRenderData()
        {
            try
            {
                return Components[ComponentType.RenderData].GetRenderData(this);
            }
            catch
            {
                throw new Exception($"{this.ToString()} was never assigned a RenderData Component.");
            }
        }
        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;

            // Remove from SpatialIndexer
            if (Collider)
            {
                OverlappedCells.ForEach(x => x.Remove(this));
            }

            // Remove from Scene
            Parent.RemoveGameObject(this);

            // Remove RenderState
            if (Components.ContainsKey(ComponentType.RenderData))
            {
                Parent.RenderActive.Remove(this);
                Parent.RenderObjects.Remove(this);
                RenderData r = (RenderData)Components[ComponentType.RenderData];
                r.GameObjectStates.Remove(this);
            }
        }
    }
}
