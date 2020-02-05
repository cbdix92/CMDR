using System;
using System.Collections.Generic;

namespace CMDR
{
    internal static partial class Physics
    {
        internal static void CheckCollision(GameObject gameObject)
        {
            // BroadPhase

            List<GameObject> Colliders = SpatialIndexer.GetNearbyColliders(gameObject);

            foreach (GameObject collider in Colliders)
            {
                // Rect Check
                if (RectCollisionCheck(gameObject, collider))
                {
                    gameObject.PhysicsConstraints.OnCollisionOccured(gameObject, collider);
                    gameObject.UnMove();
                }
                if (gameObject.Disposed)
                    return; 
            }
        }
        internal static bool RectCollisionCheck(GameObject gameObject, GameObject collider)
        {
            return gameObject.Transform.X <= collider.Transform.X + collider.Width
             && gameObject.Transform.X + gameObject.Width >= collider.Transform.X
             && gameObject.Transform.Y <= collider.Transform.Y + collider.Height
             && gameObject.Transform.Y + gameObject.Height >= collider.Transform.Y;
        }
    }
    internal static class PhysicsExtensions
    {
        internal static bool HasZeroVelocity(this GameObject gameObject)
        {
            return gameObject.Transform.Xvel == 0 && gameObject.Transform.Yvel == 0;
        }
        internal static void Move(this GameObject gameObject)
        {
            gameObject.Transform.X += gameObject.Transform.Xvel;
            gameObject.Transform.Y += gameObject.Transform.Yvel;
        }
        internal static void UnMove(this GameObject gameObject)
        {
            gameObject.Transform.X -= gameObject.Transform.Xvel;
            gameObject.Transform.Y -= gameObject.Transform.Yvel;
            gameObject.Transform.Children.ForEach(child => child.UnMove());
        }
    }
}
