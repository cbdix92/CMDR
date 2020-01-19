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
            Scene Scene = SceneManager.ActiveScene;
            foreach (GameObject GameObject in Scene.GameObjects)
            {
                Buffer.Graphics.DrawImage(GameObject.GetRenderData(), GameObject.Transform.X, GameObject.Transform.Y);
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
