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
                bool Validate = Parent.ActiveGameObjects.Contains(this);
                if (value && !Validate) Parent.ActiveGameObjects.Add(this);
                else if (!value && Validate) Parent.ActiveGameObjects.Remove(this);
                _active = value;
            }
        }
        public bool Collider { get; internal set; }
        /*private bool _collider;
        public bool Collider
        {
            get => _collider;
            set
            {
                bool Validate = Parent.ColliderGameObjects.Contains(this);

                // True
                if (value && !Validate)
                {
                    Parent.ColliderGameObjects.Add(this);
                    this.AddComponent(new PhysicsConstraints(this));
                }
                // False
                else if (!value && Validate)
                {
                    Parent.ColliderGameObjects.Remove(this);
                    this.OverlappedCells.ForEach(x => x.Remove(this));
                }

                _collider = value;

                // Make sure that the SpatialIndexer.CellSize is at least as large as the largest collider
                if (SpatialIndexer.CellSize < this.Width || SpatialIndexer.CellSize < this.Height)
                {
                    SpatialIndexer.CellSize = Math.Max(this.Width, this.Height);
                }
                SpatialIndexer.CalcGridPos(this);
            }
        }*/
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
            OverlappedCells.ForEach(x => x.Remove(this));

            // Remove from Scene
            Parent.RemoveGameObject(this);

            // Remove RenderState if any exist
            if (Components.ContainsKey(ComponentType.RenderData))
            {
                RenderData r = (RenderData)Components[ComponentType.RenderData];
                r.GameObjectStates.Remove(this);
            }
        }
    }
}
