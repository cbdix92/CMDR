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

        public static void Init(Display display)
        {
            Display = display;
            Buffer_CONTEXT = BufferedGraphicsManager.Current;
            Buffer = Buffer_CONTEXT.Allocate(Display.CreateGraphics(), new Rectangle(0, 0, Display.Width, Display.Height));
        }
        public static void ClearScreen()
        {
            Buffer.Graphics.Clear(System.Drawing.Color.White);
        }
        public static void ScreenBuffer()
        {
            Scene Scene = SceneManager.ActiveScene;
            foreach (GameObject GameObject in Scene.GameObjects)
            {
                Buffer.Graphics.DrawImage(GameObject.GetRenderData(), GameObject.Transform.X, GameObject.Transform.Y);
            }
        }
        public static void Draw()
        {
            Buffer.Render();
        }
    }
}
