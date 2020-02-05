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
    }
    internal class CollidionData
    {
        public bool[,] Data;
        public CollidionData(System.Drawing.Image image)
        {
            int Width = image.Width;
            int Height = image.Height;

            Data = new bool[Height, Width];

            System.Drawing.Bitmap Bitmap = new System.Drawing.Bitmap(image);

            for (int Y = 0; Y <= Height; Y++)
                for (int X =0; X <= Width; X++)
                {
                    Data[Y,X] = Bitmap.GetPixel(X, Y).A > BitCollider.AlphaThreshold;
                }
        }
    }
}
