using System;
using System.Collections.Generic;
using CMDR.Components;

namespace CMDR
{
    public class GameObject : IDisposable
    {
        public Scene Parent;

        #region COMPONENT_STORAGE
        public Transform Transform;
        public RenderData RenderData;
        public PhysicsConstraints PhysicsConstraints;
        #endregion

        internal List<Cell> OverlappedCells;

        private int _width;
        private int _height;

        private bool _active;

        #region PROPERTIES
        public int Hash { get => this.GetHashCode(); }

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
                    return RenderData.GetData(this).Width;
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
                    return RenderData.GetData(this).Height;
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

        #endregion

        public GameObject(Scene parent, float posX, float posY, int posZ)
        {
            Parent = parent;

            this.Use(new Transform(this, posX, posY, posZ));

            OverlappedCells = new List<Cell>();
        }
        public void Use(Component component)
        {


            switch (component.ID)
            {
                case ComponentType.Transform:
                    Transform t = (Transform)component;
                    t.Add(this);
                    this.Transform = t;
                    break;

                case ComponentType.RenderData:
                    this.RenderData = (RenderData)component;
                    this.RenderData.Add(this);
                    break;

                case ComponentType.PhysicsConstraints:
                    this.PhysicsConstraints = (PhysicsConstraints)component;
                    this.PhysicsConstraints.Add(this);
                    break;
            }
        }
        public System.Drawing.Image GetRenderData()
        {
            if (RenderData == null)
                throw new NullReferenceException("RenderData");

            return RenderData.GetData(this);
        }
        public void Dispose()
        {
            if (Disposed)
                return;

            // Remove from SpatialIndexer
            foreach (Cell cell in OverlappedCells)
                cell.Remove(this);

            // Remove from Scene
             Parent.RemoveGameObject(this);

            // Remove parent from all component objects
            if (RenderData != null)
                RenderData.Remove(this);

            if (PhysicsConstraints != null)
                PhysicsConstraints.Remove(this);

            Disposed = true;
        }
    }
}
