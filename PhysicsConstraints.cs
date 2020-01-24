using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDR
{
    public delegate void CollisionEventHandler(GameObject collider);
    
    public class PhysicsConstraints : Component
    {
        public CollisionEventHandler OnCollision;
        public bool Static { get; set; }
        public bool Collider { get; set; }
        public PhysicsConstraints(GameObject parent) : base (ComponentType.PhysicsConstraints)
        {
            Parent = parent;
        }
        public void CollisonOccured(GameObject collider)
        {
            if (OnCollision != null)
            {
                OnCollision(collider);
            }
        }
    }
}
