using System;

namespace CMDR
{
    public delegate void CollisionEventHandler(GameObject collider);
    
    public class PhysicsConstraints : Component
    {
        public CollisionEventHandler OnCollision;
        private Scene _parentScene;
        
		private bool _static;
        public bool Static
        {
            get => _static;
            set
            {
				_static = value;
                foreach (GameObject parent in Parents)
				{
					StaticLogic(parent, _static);
				}
            }
        }
		private bool _collider;
        public bool Collider
        {
            get => _collider;
            set
            {
				_collider = value;
				foreach (GameObject parent in Parents)
				{
					ColliderLogic(parent, _collider);
				}
            }
        }
        public PhysicsConstraints(Scene parentScene) : base (ComponentType.PhysicsConstraints)
        {
            _parentScene = parentScene;
        }
        public override void CollisionOccured(GameObject collider)
        {
            if (OnCollision != null)
            {
                OnCollision(collider);
            }
        }
		private void StaticLogic(GameObject newParent, bool val)
		{
			// Set True
			if (val && !Collider)
			{
				_static = true;
				// All static GameObjects must also be colliders
				_collider = true;
				SpatialIndexer.CalcGridPos(newParent);
			}
			// Collider was set before Static! This can cause issues with the SpatialIndexer!
			else if (val && Collider && !Static)
			{
				throw new Exception("Collider flag may not be set before the Static flag.\n Doing this can cause issues with the SpatialIndexer.\n Instead just simply set the static flag and the collider flag will be set internally.");
			}
			// Set False
			else if (!val && Static)
			{
				_static = false;
				Collider = false;
			}
			newParent.Static = val;
		}
		private void ColliderLogic(GameObject newParent, bool val)
		{
			// Set True
			// Ensure that the newParent has not already been set true
			if (val && !newParent.Collider)
			{
				_parentScene.ColliderGameObjects.Add(newParent);
				// Make sure that the SpatialIndexer.CellSize is at least as large as the largest collider
				if (SpatialIndexer.CellSize < Math.Max(newParent.Width, newParent.Height) && !_static)
				{
					SpatialIndexer.CellSize = Math.Max(newParent.Width, newParent.Height);
				}
				else
				{
					SpatialIndexer.CalcGridPos(newParent);
				}
			}
			// Set False
			// Ensure that the newParent has not already been set false
			else if (!val && newParent.Collider)
			{
				_parentScene.ColliderGameObjects.Remove(newParent);
				newParent.OverlappedCells.ForEach(x => x.Remove(newParent));
			}
			newParent.Collider = val;
		}
		internal override void NewParent(GameObject newParent)
		{
			Parents.Add(newParent);
			StaticLogic(newParent, _static);
			ColliderLogic(newParent, _collider);
		}
    }
}
