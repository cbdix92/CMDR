using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDR
{
    internal static class BitCollider
    {
        public static byte AlphaThreshold = 60;

        public static bool BitCollide(GameObject gameObject, GameObject collider)
        {
            bool result = false;

            int P1X = (int)Math.Min(gameObject.Transform.X, collider.Transform.X);
            int P2X = (int)Math.Max(gameObject.Transform.X, collider.Transform.X);

            int P1Y = (int)Math.Min(gameObject.Transform.Y, collider.Transform.Y);
            int P2Y = (int)Math.Max(gameObject.Transform.Y, collider.Transform.Y);

            int BoundSize = (P2X - P1X) * (P2Y - P1Y);

            int BoundWidth = P2X - P1X;
            int BoundHeight = P2Y - P1Y;

            for (int i = 0; i < BoundSize; i++)
            {
                if (i <= BoundWidth - 1)
                    break;// To be continued ...
            }



            return result;
        }
    }
    internal class CollisionData
    {
        public bool[] Data;
        public CollisionData(System.Drawing.Image image)
        {
            int Width = image.Width;

            int DataSize = Width * image.Height;


            Data = new bool[DataSize];

            System.Drawing.Bitmap Bitmap = new System.Drawing.Bitmap(image);

            for (int i = 0; i <= DataSize; i++)
            {
                if (i <= Width - 1)
                {
                    Data[i] = Bitmap.GetPixel(i % Width - 1, 0).A > BitCollider.AlphaThreshold;
                    continue;
                }

                Data[i] = Bitmap.GetPixel(i % Width - 1, Math.Abs(i / Width)).A > BitCollider.AlphaThreshold;
            }
        }
    }
}
