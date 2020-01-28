using System;

namespace CMDR
{
    public delegate void CollisionEventHandler(GameObject collider);
    
    public class PhysicsConstraints : Component
    {
        public CollisionEventHandler OnCollision;
        private Scene _parentScene;
        private bool _static;
        private bool _collider;
        public bool Static { get; set; }
        public bool Collider
        {
            get => _collider;
            set
            {
                bool Validate = Parent.Parent.ColliderGameObjects.Contains(Parent);

                // Set True
                if(value && !Validate)
                {
                    Parent.Parent.ColliderGameObjects.Add(Parent.Parent);
                    // Make sure that the SpatialIndexer.CellSize is at least as large as the largest collider
                    if (SpatialIndexer.CellSize < Math.Max(Parent.Width, Parent.Height))
                    {
                        SpatialIndexer.CellSize = Math.Max(Parent.Width, Parent.Height);
                    }
                }
                // Set False
                else if (!value && validate)
                {
                    Parent.Parent.ColliderGameObjects.Remove(Parent);
                    Parent.OverlappedCells.ForEach(x => x.Remove(Parent));
                }

                _collider = value;
            }
        }
        public PhysicsConstraints(GameObject parent, Scene parentScene) : base (ComponentType.PhysicsConstraints)
        {
            Parent = parent;
            _parentScene = parentScene;
        }
        public override void CollisionOccured(GameObject collider)
        {
            if (OnCollision != null)
            {
                OnCollision(collider);
            }
        }
    }
}
