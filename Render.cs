using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CMDR
{
    internal static partial class Render
    {
        public static Graphics GFX;
        internal static Form Display;

        public static int ZDepth { get; set; }
        public static void ClearScreen()
        {
            GFX.Clear(System.Drawing.Color.White);
        }
        public static void Draw()
        {

            ClearScreen();
            Scene Scene = SceneManager.ActiveScene;
            foreach(GameObject GameObject in Scene.GameObjects)
            {
                GFX.DrawImage(GameObject.GetRenderData(), GameObject.Transform.X, GameObject.Transform.Y);
            }
        }
    }
}
