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
        public bool Static
        {
            get => _static;
            set
            {
                // All static GameObjects must also be colliders
                if (value && !Collider)
                {
                    Collider  = true;
                }
                // Collider was set first. The GameObject may have resized the SpatialIndexer.
                // If so, it needs to be reverted back
                else if (value && Collider && !_static)
                {
                    _static = value;
                    int largestInt;
                    foreach (GameObject collider in _parentScene.ColliderGameObjects)
                    {
                        if(!collider.Components[ComponentType.PhysicsConstraints].GetStatic())
                        {
                            int largestSize = Math.Max(collider.Width, collider.Height);
                            largestInt = Math.Max(largestInt, largestSize);
                        }
                        
                    }
                    if (SpatialIndexer.CellSize > largestInt)
                    {
                        SpatialIndexer.CellSize = largestInt;
                    }
                }
                _static = value;
            }
        }
        public bool Collider
        {
            get => _collider;
            set
            {
                bool Validate = _parentScene.ColliderGameObjects.Contains(Parent);

                // Set True
                if(value && !Validate)
                {
                    _parentScene.ColliderGameObjects.Add(Parent);
                    // Make sure that the SpatialIndexer.CellSize is at least as large as the largest collider
                    if (SpatialIndexer.CellSize < Math.Max(Parent.Width, Parent.Height) && !Collider)
                    {
                        SpatialIndexer.CellSize = Math.Max(Parent.Width, Parent.Height);
                    }
                }
                // Set False
                else if (!value && validate)
                {
                    _parentScene.ColliderGameObjects.Remove(Parent);
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
        public override bool GetStatic()
        {
            return Static;
        }
    }
}
