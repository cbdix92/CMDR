using System;
using System.Collections.Generic;

namespace CMDR
{
    internal static partial class Physics
    {
        internal static void CheckCollision(GameObject gameObject)
        {
            // BroadPhase

            // Get possible Colliders with "gameObject" via SpatialIndexer
            List<GameObject> Colliders = gameObject.GetNearbyColliders();

            foreach (GameObject collider in Colliders)
            {

                // Rect Check
                if (RectCollisionCheck(gameObject, collider))
                {
                    gameObject.PhysicsConstraints.OnCollisionOccured(gameObject, collider);
                    gameObject.UnMove();
                }

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
        internal static void Move(this GameObject GameObject)
        {
            GameObject.Transform.X += GameObject.Transform.Xvel;
            GameObject.Transform.Y += GameObject.Transform.Yvel;
        }
        internal static void UnMove(this GameObject GameObject)
        {
            GameObject.Transform.X -= GameObject.Transform.Xvel;
            GameObject.Transform.Y -= GameObject.Transform.Yvel;
        }
    }
}
