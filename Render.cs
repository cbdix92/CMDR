using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CMDR
{
    internal static partial class Render
    {
        internal static Form Display;
        internal static BufferedGraphics Buffer;
        internal static BufferedGraphicsContext Buffer_CONTEXT;

        public static int ZDepth { get; set; }

        internal static void ClearScreen()
        {
            Buffer.Graphics.Clear(System.Drawing.Color.Black);
        }
        internal static void ScreenBuffer()
        {
            // Draw SpatialIndexer Grid Lines
            foreach ((int,int) i in SpatialIndexer.GridCells.Keys)
            {
                int s = SpatialIndexer.CellSize;
                int x = i.Item2 * s;
                int y = i.Item1 * s;
                int cx = x + s / 2;
                int cy = y + s / 2;
                Buffer.Graphics.DrawRectangle(new Pen(Brushes.Red), new Rectangle(x-(int)Camera.X, y-(int)Camera.Y, s, s));
                Buffer.Graphics.DrawString(SpatialIndexer.GridCells[i].Cache.Count.ToString(), new Font(Display.Font, FontStyle.Regular), Brushes.Red, cx-Camera.X, cy-Camera.Y);
            }

            // Draw GameObjects to the buffer
            Scene Scene = SceneManager.ActiveScene;
            foreach (GameObject GameObject in Scene.RenderActive)
            {
                if (GameObject != null)
                {
                    Buffer.Graphics.DrawImage(GameObject.GetRenderData(), GameObject.Transform.X-Camera.X, GameObject.Transform.Y-Camera.Y);
                }
            }

        }
        internal static void SetDisplay(Display display)
        {
            Display = display;
            Buffer_CONTEXT = BufferedGraphicsManager.Current;
            Buffer = Buffer_CONTEXT.Allocate(Display.CreateGraphics(), new Rectangle(0, 0, Display.Width, Display.Height));
        }
        internal static void Draw()
        {
            Buffer.Render();
        }
    }
}
