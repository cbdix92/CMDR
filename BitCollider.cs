using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDR
{
    public static class BitCollider
    {
        public static byte AlphaThreshold = 60;

        internal static bool BitColliderCheck(GameObject gameObject, GameObject collider)
        {
            bool Result = false;
			
			float OBJ1X = gameObject.Transform.X;
			float OBJ1Y = gameObject.Transform.Y;
			
			float OBJ2X = collider.Transform.X;
			float OBJ2Y = collider.Transform.Y;

            int P1X = (int)Math.Min(gameObject.Transform.X, collider.Transform.X);
			int P1Y = (int)Math.Min(gameObject.Transform.Y, collider.Transform.Y);
            
			int P2X = (int)Math.Max(gameObject.Transform.X, collider.Transform.X);
            int P2Y = (int)Math.Max(gameObject.Transform.Y, collider.Transform.Y);

            int BoundSize = (P2X - P1X) * (P2Y - P1Y);

            int Width = P2X - P1X;
			
			CollisionData ColData1 = gameObject.PhysicsConstraints.ColliderData;
			CollisionData ColData2 = collider.PhysicsConstraints.ColliderData;

            for (int i = 0; i < BoundSize - 1; i++)
            {
                if (ColData1.Data[(i / Width) - P1Y - OBJ1Y,(i % Width) - P1X - OBJ1X] 
					&& 
					ColData2.Data[(i / Width) - P1Y - OBJ1Y,(i % Width) - P1X - OBJ2X])
					Result = true;
            }



            return Result;
        }
    }
    internal class CollisionData
    {
        public bool[,] Data;
		
        public CollisionData(System.Drawing.Image image)
        {
            int Width = image.Width;
			int Height = image.Height;

            Data = new bool[Height, Width];

            System.Drawing.Bitmap Bitmap = new System.Drawing.Bitmap(image);

            for (int Y = 0; Y <= Height; Y++)
				for (int X = 0; X <= Width; X++)
				{
					Data[Y,X] = Bitmap.GetPixel(X,Y).A > BitCollider.AlphaThreshold;
				}
        }
    }
}
